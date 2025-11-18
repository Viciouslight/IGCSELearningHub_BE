using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Accounts.Interfaces
{
    public interface IAccountProfileService
    {
        Task<ApiResult<AccountDTO>> UpdateProfileAsync(int accountId, UpdateAccountDTO updateDto);
        Task<ApiResult<string>> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<ApiResult<string>> SendPasswordResetEmailAsync(ForgotPasswordRequestDTO dto, string origin, CancellationToken ct = default);
    }
}
