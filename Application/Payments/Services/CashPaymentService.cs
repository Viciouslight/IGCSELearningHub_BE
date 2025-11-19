using IGCSELearningHub.Application.Payments.DTOs;
using IGCSELearningHub.Application.Payments.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentMethods.Helpers;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Entities;
using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Payments.Entities;
using IGCSELearningHub.Domain.Payments.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IGCSELearningHub.Application.Payments.Services
{
    public class CashPaymentService : ICashPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTimeProvider _clock;
        private readonly ILogger<CashPaymentService> _logger;
        private readonly IEnrollmentAdminService? _enrollmentService;

        public CashPaymentService(
            IUnitOfWork uow,
            IDateTimeProvider clock,
            ILogger<CashPaymentService> logger,
            IEnrollmentAdminService? enrollmentService = null)
        {
            _uow = uow;
            _clock = clock;
            _logger = logger;
            _enrollmentService = enrollmentService;
        }

        public async Task<ApiResult<CashPaymentResultDTO>> ProcessAsync(int orderId, CashPaymentRequestDTO request)
        {
            request ??= new CashPaymentRequestDTO();

            var order = await _uow.OrderRepository
                .GetAllQueryable($"{nameof(Order.Payments)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return ApiResult<CashPaymentResultDTO>.Fail("OrderIndex not found.", 404);
            }

            var amount = request.Amount ?? order.TotalAmount;
            if (amount <= 0)
            {
                return ApiResult<CashPaymentResultDTO>.Fail("Amount must be greater than 0.", 400);
            }

            var cashMethod = await PaymentMethodUtilities.EnsureMethodAsync(
                _uow,
                PaymentMethodUtilities.CashMethodName,
                "Thanh toán tiền mặt",
                true,
                _logger);
            if (!cashMethod.IsActive)
            {
                return ApiResult<CashPaymentResultDTO>.Fail("Cash payment method is inactive.", 409);
            }

            // if order already paid, return latest payment info
            if (order.Status == OrderStatus.Paid)
            {
                var latestPayment = order.Payments
                    .Where(p => !p.IsDeleted && p.Status == PaymentStatus.Paid)
                    .OrderByDescending(p => p.PaidDate ?? p.ModifiedAt)
                    .FirstOrDefault();

                if (latestPayment != null)
                {
                    var dto = new CashPaymentResultDTO
                    {
                        PaymentId = latestPayment.Id,
                        OrderId = order.Id,
                        Amount = latestPayment.Amount,
                        Status = latestPayment.Status.ToString(),
                        PaidDate = latestPayment.PaidDate ?? latestPayment.ModifiedAt,
                        Method = latestPayment.PaymentMethod?.PaymentMethodName ?? PaymentMethodUtilities.CashMethodName
                    };
                    return ApiResult<CashPaymentResultDTO>.Success(dto, "OrderIndex already paid.");
                }

                // fallback if no payment record (should not happen)
                var fallback = new CashPaymentResultDTO
                {
                    PaymentId = 0,
                    OrderId = order.Id,
                    Amount = amount,
                    Status = PaymentStatus.Paid.ToString(),
                    PaidDate = order.ModifiedAt,
                    Method = PaymentMethodUtilities.CashMethodName
                };
                return ApiResult<CashPaymentResultDTO>.Success(fallback, "OrderIndex already paid.");
            }

            using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Recording cash payment for order {OrderId} with amount {Amount}. Note: {Note}", orderId, amount, request.Note);

                // cancel any pending payments
                var pending = order.Payments.Where(p => !p.IsDeleted && p.Status == PaymentStatus.Pending).ToList();
                foreach (var payment in pending)
                {
                    payment.Status = PaymentStatus.Canceled;
                    payment.ModifiedAt = _clock.UtcNow;
                    _uow.PaymentRepository.Update(payment);
                }

                var cashPayment = new Payment
                {
                    OrderId = order.Id,
                    PaymentMethodId = cashMethod.Id,
                    Amount = amount,
                    Status = PaymentStatus.Paid,
                    Channel = PaymentChannel.Web,
                    PaidDate = _clock.UtcNow
                };
                await _uow.PaymentRepository.AddAsync(cashPayment);

                order.Status = OrderStatus.Paid;
                _uow.OrderRepository.Update(order);

                await _uow.SaveChangesAsync();
                await transaction.CommitAsync();

                var resultDto = new CashPaymentResultDTO
                {
                    PaymentId = cashPayment.Id,
                    OrderId = order.Id,
                    Amount = cashPayment.Amount,
                    Status = cashPayment.Status.ToString(),
                    PaidDate = cashPayment.PaidDate ?? _clock.UtcNow,
                    Method = cashMethod.PaymentMethodName
                };

                if (_enrollmentService != null)
                {
                    try
                    {
                        await _enrollmentService.CreateFromOrderAsync(order.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to create enrollments for order {OrderId} after cash payment.", order.Id);
                    }
                }

                return ApiResult<CashPaymentResultDTO>.Success(resultDto, "Cash payment recorded successfully.", 201);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Cash payment failed for order {OrderId}.", orderId);
                return ApiResult<CashPaymentResultDTO>.Fail("Failed to record cash payment.", 500)
                    .SetSystemError(ex.Message);
            }
        }
    }
}
