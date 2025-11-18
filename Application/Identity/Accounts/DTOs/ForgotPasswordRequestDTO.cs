using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Identity.Accounts.DTOs
{
    public class ForgotPasswordRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
