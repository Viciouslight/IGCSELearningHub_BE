using IGCSELearningHub.Application.DTOs.Orders;

namespace IGCSELearningHub.Application.Orders.OrderPlacement.DTOs
{
    public class CreateOrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
