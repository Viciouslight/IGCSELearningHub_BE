using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Accounts.Interfaces
{
    public interface IAccountAdminService
    {
        Task<ApiResult<AccountDTO>> GetAccountByIdAsync(int id);
        Task<ApiResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<PagedResult<AccountDTO>> GetAccountsPagedAsync(string? q, string? role, string? status, int page, int pageSize, string? sort);
        Task<ApiResult<bool>> CheckUsernameOrEmailExistsAsync(string username, string email);
        Task<ApiResult<string>> BanAccountAsync(int accountId);
        Task<ApiResult<string>> UnbanAccountAsync(int accountId);
    }
}
