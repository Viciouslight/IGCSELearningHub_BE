using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.OrderPricing.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.OrderPricing.Interfaces
{
    public interface IOrderPricingService
    {
        Task<ApiResult<OrderPriceQuote>> BuildPriceQuoteAsync(int accountId, IEnumerable<OrderItemRequest> items);
    }
}
