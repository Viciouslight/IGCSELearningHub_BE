using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Payments.PaymentMethods.DTOs
{
    public class PaymentMethodStatusUpdateDTO
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
