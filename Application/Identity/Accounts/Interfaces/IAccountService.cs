using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Accounts.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResult<AccountDTO?>> GetAccountByIdAsync(int id);
        Task<ApiResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<PagedResult<AccountDTO>> GetAccountsPagedAsync(string? q, string? role, string? status, int page, int pageSize, string? sort);
        Task<ApiResult<bool>> CheckUsernameOrEmailExistsAsync(string username, string email);
        Task<ApiResult<AccountDTO?>> UpdateAccountAsync(int accountId, UpdateAccountDTO updateDto);
        Task<ApiResult<string>> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<ApiResult<string>> SendPasswordResetEmailAsync(ForgotPasswordRequestDTO dto, string origin, CancellationToken ct = default);
        Task<ApiResult<string>> BanAccountAsync(int accountId);
        Task<ApiResult<string>> UnbanAccountAsync(int accountId);
    }
}
