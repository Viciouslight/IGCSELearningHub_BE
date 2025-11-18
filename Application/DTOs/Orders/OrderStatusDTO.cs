namespace IGCSELearningHub.Application.DTOs.Orders
{
    public class OrderStatusDTO
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = null!;        // Pending | Paid | Canceled | ...
        public decimal TotalAmount { get; set; }
        public PaymentStatusDTO? LastPayment { get; set; }
    }
}
