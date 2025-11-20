using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Services
{
    public class OrderLifecycleService : IOrderLifecycleService
    {
        private readonly IUnitOfWork _uow;

        public OrderLifecycleService(IUnitOfWork uow)
        {
            _uow = uow;
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
    }
}
