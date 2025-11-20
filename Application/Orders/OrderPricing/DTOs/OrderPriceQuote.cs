using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Orders.OrderPricing.DTOs
{
    public class OrderPriceQuote
    {
        public IList<PricedOrderItem> Items { get; set; } = new List<PricedOrderItem>();
        public decimal TotalAmount { get; set; }
    }
}
