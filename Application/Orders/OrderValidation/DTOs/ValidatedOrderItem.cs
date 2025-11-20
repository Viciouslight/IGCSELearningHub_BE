using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Orders.OrderValidation.DTOs
{
    public class ValidatedOrderItem
    {
        public ItemType ItemType { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Title { get; set; }
    }
}
