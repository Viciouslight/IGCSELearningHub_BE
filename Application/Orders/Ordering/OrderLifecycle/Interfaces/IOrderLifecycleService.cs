using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Interfaces
{
    public interface IOrderLifecycleService
    {
        Task<ApiResult<OrderStatusDTO>> MarkPaidAsync(int orderId, string? reason = null);
        Task<ApiResult<OrderStatusDTO>> MarkFailedAsync(int orderId, string? reason = null);
        Task<ApiResult<OrderStatusDTO>> CancelPendingAsync(int orderId, string? reason = null);
    }
}
