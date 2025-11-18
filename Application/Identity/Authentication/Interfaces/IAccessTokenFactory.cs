using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Identity.Enums;

namespace IGCSELearningHub.Application.Identity.Authentication.Interfaces
{
    public interface IAccessTokenFactory
    {
        ApiResult<string> GenerateAccessToken(int accountId, string accountUserName, AccountRole role, string? accountStatus);
    }
}

