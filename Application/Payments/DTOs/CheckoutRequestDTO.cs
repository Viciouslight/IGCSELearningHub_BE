using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Application.Payments.DTOs
{
    public class CheckoutRequestDTO
    {
        public PaymentChannel? Channel { get; set; }
    }
}
