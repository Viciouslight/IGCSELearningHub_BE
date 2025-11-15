using Application.Authentication.DTOs;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<ApiResult<AuthenticatedUserDTO>> IssueAsync(Account account, string? ipAddress = null);
        Task<ApiResult<AuthenticatedUserDTO>> RefreshAsync(string refreshToken, string? ipAddress = null);
        Task<bool> RevokeByTokenAsync(string refreshToken, string? reason, string? ipAddress = null);
        Task<int> RevokeAllForAccountAsync(int accountId, string? reason, string? ipAddress = null);
    }
}
