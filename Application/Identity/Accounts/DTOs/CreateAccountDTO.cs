namespace IGCSELearningHub.Application.Identity.Accounts.DTOs
{
    public class CreateAccountDTO
    {
        public string UserName { get; set; } = null!;

        public string? FullName { get; set; }

        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Role { get; set; }

        public string? Status { get; set; }

        public bool IsExternal { get; set; } = false;

        public string? ExternalProvider { get; set; }
    }
}
