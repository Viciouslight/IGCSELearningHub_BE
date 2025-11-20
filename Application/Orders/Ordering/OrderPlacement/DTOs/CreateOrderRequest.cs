using IGCSELearningHub.Application.DTOs.Orders;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPlacement.DTOs
{
    public class CreateOrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
