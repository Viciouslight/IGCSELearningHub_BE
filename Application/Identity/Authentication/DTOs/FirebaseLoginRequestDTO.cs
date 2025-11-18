using System.ComponentModel.DataAnnotations;

namespace IGCSELearningHub.Application.Identity.Authentication.DTOs
{
    public class FirebaseLoginRequestDTO
    {
        [Required]
        public string FirebaseIdToken { get; set; } = string.Empty;
    }
}
