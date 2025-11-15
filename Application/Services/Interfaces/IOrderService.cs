using Application.DTOs.Orders;
using Application.Wrappers;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResult<OrderSummaryDTO>> CreateOrderAsync(int accountId, CreateOrderRequest req);
        Task<ApiResult<OrderSummaryDTO>> GetOrderAsync(int accountId, int orderId);
        Task<PagedResult<MyOrderListItemDTO>> GetMyOrdersAsync(int accountId, int page, int pageSize);
    }
}
