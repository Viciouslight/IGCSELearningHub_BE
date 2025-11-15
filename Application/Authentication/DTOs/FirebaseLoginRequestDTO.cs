using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.DTOs
{
    public class FirebaseLoginRequestDTO
    {
        [Required]
        public string FirebaseIdToken { get; set; } = string.Empty;
    }
}
