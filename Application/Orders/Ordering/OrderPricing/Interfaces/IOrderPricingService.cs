using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.Ordering.OrderPricing.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPricing.Interfaces
{
    public interface IOrderPricingService
    {
        Task<ApiResult<OrderPriceQuote>> BuildPriceQuoteAsync(int accountId, IEnumerable<OrderItemRequest> items);
    }
}
