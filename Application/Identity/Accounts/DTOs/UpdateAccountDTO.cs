using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Identity.Accounts.DTOs
{
    public class UpdateAccountDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
    }
}
