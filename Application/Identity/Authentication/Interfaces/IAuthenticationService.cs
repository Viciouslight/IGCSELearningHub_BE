using IGCSELearningHub.Application.Identity.Authentication.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResult<AuthenticatedUserDTO>> RegisterAsync(AccountRegistrationDTO registrationDto);
        Task<ApiResult<AuthenticatedUserDTO>> LoginAsync(AccountLoginDTO loginDto);
        Task<ApiResult<AuthenticatedUserDTO>> LoginWithFirebaseAsync(FirebaseLoginRequestDTO dto);
        Task<ApiResult<AuthenticatedUserDTO>> RefreshAsync(RefreshTokenRequestDTO request, string? ipAddress = null);
    }
}
