namespace IGCSELearningHub.Application.Identity.Accounts.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? Status { get; set; }
        public bool IsExternal { get; set; }
        public string? ExternalProvider { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
