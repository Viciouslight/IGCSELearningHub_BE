using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Notifications;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Interfaces;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Identity.Devices.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Services
{
    public class OrderLifecycleService : IOrderLifecycleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IEnrollmentAdminService? _enrollmentService;
        private readonly IInvoiceIssuingService? _invoiceIssuing;
        private readonly IPushNotificationService? _pushNotifications;
        private readonly IPaymentRealtimeNotifier? _realtimeNotifier;
        private readonly IDeviceService? _deviceService;

        public OrderLifecycleService(
            IUnitOfWork uow,
            IEnrollmentAdminService? enrollmentService = null,
            IInvoiceIssuingService? invoiceIssuing = null,
            IPushNotificationService? pushNotifications = null,
            IPaymentRealtimeNotifier? realtimeNotifier = null,
            IDeviceService? deviceService = null)
        {
            _uow = uow;
            _enrollmentService = enrollmentService;
            _invoiceIssuing = invoiceIssuing;
            _pushNotifications = pushNotifications;
            _realtimeNotifier = realtimeNotifier;
            _deviceService = deviceService;
        }

        public async Task<ApiResult<OrderStatusDTO>> MarkPaidAsync(int orderId, string? reason = null)
        {
            var order = await _uow.OrderRepository.GetAllQueryable($"{nameof(Order.Payments)},{nameof(Order.OrderDetails)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return ApiResult<OrderStatusDTO>.Fail("OrderIndex not found.", 404);

            if (order.Status == OrderStatus.Paid)
                return ApiResult<OrderStatusDTO>.Success(BuildStatus(order), "OrderIndex already paid.");

            order.Status = OrderStatus.Paid;
            _uow.OrderRepository.Update(order);
            await _uow.SaveChangesAsync();

            await RunPostPaymentAsync(order);

            return ApiResult<OrderStatusDTO>.Success(BuildStatus(order), reason ?? "OrderIndex marked as paid.");
        }

        public async Task<ApiResult<OrderStatusDTO>> MarkFailedAsync(int orderId, string? reason = null)
        {
            var order = await _uow.OrderRepository.GetAllQueryable($"{nameof(Order.Payments)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return ApiResult<OrderStatusDTO>.Fail("OrderIndex not found.", 404);

            if (order.Status == OrderStatus.Failed)
                return ApiResult<OrderStatusDTO>.Success(BuildStatus(order), "OrderIndex already failed.");

            order.Status = OrderStatus.Failed;
            _uow.OrderRepository.Update(order);
            await _uow.SaveChangesAsync();
            return ApiResult<OrderStatusDTO>.Success(BuildStatus(order), reason ?? "OrderIndex marked as failed.");
        }

        public async Task<ApiResult<OrderStatusDTO>> CancelPendingAsync(int orderId, string? reason = null)
        {
            var order = await _uow.OrderRepository.GetAllQueryable($"{nameof(Order.Payments)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return ApiResult<OrderStatusDTO>.Fail("OrderIndex not found.", 404);

            if (order.Status != OrderStatus.Pending)
                return ApiResult<OrderStatusDTO>.Fail("Only pending orders can be canceled.", 400);

            order.Status = OrderStatus.Failed;
            _uow.OrderRepository.Update(order);
            await _uow.SaveChangesAsync();
            return ApiResult<OrderStatusDTO>.Success(BuildStatus(order), reason ?? "OrderIndex canceled.");
        }

        private static OrderStatusDTO BuildStatus(Order order)
        {
            var lastPay = order.Payments
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.ModifiedAt)
                .FirstOrDefault();

            return new OrderStatusDTO
            {
                OrderId = order.Id,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                LastPayment = lastPay == null
                    ? null
                    : new PaymentStatusDTO
                    {
                        Amount = lastPay.Amount,
                        Status = lastPay.Status.ToString(),
                        PaidDate = lastPay.PaidDate,
                        Method = lastPay.PaymentMethod?.PaymentMethodName ?? ""
                    }
            };
        }

        private async Task RunPostPaymentAsync(Order order)
        {
            // Enrollment
            if (_enrollmentService != null)
            {
                try
                {
                    await _enrollmentService.CreateFromOrderAsync(order.Id);
                }
                catch (Exception ex)
                {
                    // best-effort, log only
                    Console.WriteLine($"Enrollment post-payment failed for order {order.Id}: {ex.Message}");
                }
            }

            // Invoice issuing
            if (_invoiceIssuing != null)
            {
                try
                {
                    await _invoiceIssuing.IssueInvoiceAsync(order.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Auto-invoice failed for order {order.Id}: {ex.Message}");
                }
            }

            // Notifications
            if (_realtimeNotifier != null)
            {
                try
                {
                    await _realtimeNotifier.NotifyPaymentSuccessAsync(order.AccountId, order.Id, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Realtime notify failed for order {order.Id}: {ex.Message}");
                }
            }

            if (_pushNotifications != null && _deviceService != null)
            {
                try
                {
                    var tokens = await _deviceService.GetActiveDeviceTokensAsync(order.AccountId, CancellationToken.None);
                    if (tokens.Count > 0)
                    {
                        await _pushNotifications.SendPaymentSuccessAsync(order.AccountId, order.Id, tokens, CancellationToken.None);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Push notify failed for order {order.Id}: {ex.Message}");
                }
            }
        }
    }
}
