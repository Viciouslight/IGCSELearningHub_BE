using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPricing.DTOs
{
    public class PricedOrderItem
    {
        public ItemType ItemType { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
