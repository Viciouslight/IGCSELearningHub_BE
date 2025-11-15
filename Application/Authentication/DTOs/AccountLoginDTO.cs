using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.DTOs
{
    public class AccountLoginDTO
    {
        [Required]
        public string EmailOrUsername { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
