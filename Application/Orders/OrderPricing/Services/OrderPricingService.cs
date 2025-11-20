using IGCSELearningHub.Application.DTOs.Orders;
using IGCSELearningHub.Application.Orders.OrderPricing.DTOs;
using IGCSELearningHub.Application.Orders.OrderPricing.Interfaces;
using IGCSELearningHub.Application.Orders.OrderValidation.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Orders.OrderPricing.Services
{
    public class OrderPricingService : IOrderPricingService
    {
        private readonly IOrderValidationService _validation;

        public OrderPricingService(IOrderValidationService validation)
        {
            _validation = validation;
        }

        public async Task<ApiResult<OrderPriceQuote>> BuildPriceQuoteAsync(int accountId, IEnumerable<OrderItemRequest> items)
        {
            var validation = await _validation.ValidateItemsAsync(accountId, items);
            if (!validation.Succeeded)
                return ApiResult<OrderPriceQuote>.Fail(validation.Message ?? "Validation failed.", validation.StatusCode);

            var pricedItems = validation.Data!
                .Select(v => new PricedOrderItem
                {
                    ItemType = v.ItemType,
                    ItemId = v.ItemId,
                    Quantity = v.Quantity,
                    UnitPrice = v.UnitPrice
                })
                .ToList();

            var total = pricedItems.Sum(i => i.UnitPrice * i.Quantity);

            var quote = new OrderPriceQuote
            {
                Items = pricedItems,
                TotalAmount = total
            };

            return ApiResult<OrderPriceQuote>.Success(quote);
        }
    }
}
