namespace IGCSELearningHub.Application.Payments.PaymentOrchestration.DTOs
{
    public sealed class PaymentCheckoutDTO
    {
        public string CheckoutUrl { get; set; } = default!;
        public string Provider { get; set; } = "VNPay";
    }
}
