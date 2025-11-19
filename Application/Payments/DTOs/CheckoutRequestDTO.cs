using IGCSELearningHub.Domain.Payments.Enums;

namespace IGCSELearningHub.Application.Payments.DTOs
{
    public class CheckoutRequestDTO
    {
        public PaymentChannel? Channel { get; set; }
    }
}
