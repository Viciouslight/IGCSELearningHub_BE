namespace IGCSELearningHub.Application.Identity.Authentication.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string passwordHash);
    }
}
