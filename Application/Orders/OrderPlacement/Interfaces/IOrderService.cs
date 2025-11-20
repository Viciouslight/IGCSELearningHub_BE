using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Application.Orders.OrderPlacement.DTOs;

namespace IGCSELearningHub.Application.Orders.OrderPlacement.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResult<OrderSummaryDTO>> CreateOrderAsync(int accountId, CreateOrderRequest req);
        Task<ApiResult<OrderSummaryDTO>> GetOrderAsync(int accountId, int orderId);
        Task<PagedResult<MyOrderListItemDTO>> GetMyOrdersAsync(int accountId, int page, int pageSize);
    }
}
