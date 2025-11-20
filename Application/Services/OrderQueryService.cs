using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Services.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IUnitOfWork _uow;
        public OrderQueryService(IUnitOfWork uow) => _uow = uow;

        public async Task<ApiResult<OrderStatusDTO>> GetOrderStatusAsync(int accountId, int orderId)
        {
            var o = await _uow.OrderRepository.GetAllQueryable(
                    $"{nameof(Order.Payments)},{nameof(Order.Account)},{nameof(Order.OrderDetails)}")
                .FirstOrDefaultAsync(x => x.Id == orderId && x.AccountId == accountId);

            if (o == null) return ApiResult<OrderStatusDTO>.Fail("OrderIndex not found.", 404);

            var lastPay = o.Payments
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.ModifiedAt)
                .FirstOrDefault();

            var dto = new OrderStatusDTO
            {
                OrderId = o.Id,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                LastPayment = lastPay == null ? null : new PaymentStatusDTO
                {
                    Amount = lastPay.Amount,
                    Status = lastPay.Status.ToString(),
                    PaidDate = lastPay.PaidDate,
                    Method = lastPay.PaymentMethod?.PaymentMethodName ?? ""
                }
            };

            return ApiResult<OrderStatusDTO>.Success(dto);
        }
    }
}
