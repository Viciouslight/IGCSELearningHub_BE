namespace IGCSELearningHub.Application.DTOs.Orders
{
    public class CreateOrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
