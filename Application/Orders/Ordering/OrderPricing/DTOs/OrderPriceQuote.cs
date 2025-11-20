namespace IGCSELearningHub.Application.Orders.Ordering.OrderPricing.DTOs
{
    public class OrderPriceQuote
    {
        public IList<PricedOrderItem> Items { get; set; } = new List<PricedOrderItem>();
        public decimal TotalAmount { get; set; }
    }
}
