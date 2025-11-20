using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.Ordering.OrderPlacement.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPlacement.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResult<OrderSummaryDTO>> CreateOrderAsync(int accountId, CreateOrderRequest req);
        Task<ApiResult<OrderSummaryDTO>> GetOrderAsync(int accountId, int orderId);
        Task<PagedResult<MyOrderListItemDTO>> GetMyOrdersAsync(int accountId, int page, int pageSize);
    }
}
