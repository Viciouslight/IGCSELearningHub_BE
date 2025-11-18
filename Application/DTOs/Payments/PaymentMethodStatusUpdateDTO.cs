using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.DTOs.Payments
{
    public class PaymentMethodStatusUpdateDTO
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
