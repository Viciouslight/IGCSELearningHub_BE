using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderQueries.Interfaces
{
    public interface IOrderQueryService
    {
        Task<ApiResult<OrderStatusDTO>> GetOrderStatusAsync(int accountId, int orderId);
    }
}
