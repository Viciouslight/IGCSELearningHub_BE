using Application.Authentication.DTOs;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResult<AuthenticatedUserDTO>> RegisterAsync(AccountRegistrationDTO registrationDto);
        Task<ApiResult<AuthenticatedUserDTO>> LoginAsync(AccountLoginDTO loginDto);
        Task<ApiResult<AuthenticatedUserDTO>> LoginWithFirebaseAsync(FirebaseLoginRequestDTO dto);
        Task<ApiResult<AuthenticatedUserDTO>> RefreshAsync(RefreshTokenRequestDTO request, string? ipAddress = null);
    }
}
