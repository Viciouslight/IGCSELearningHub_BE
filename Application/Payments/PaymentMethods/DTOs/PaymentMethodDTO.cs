namespace IGCSELearningHub.Application.Payments.PaymentMethods.DTOs
{
    public class PaymentMethodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
