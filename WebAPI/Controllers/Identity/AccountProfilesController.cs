using Asp.Versioning;
using IGCSELearningHub.Application.Exceptions;
using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Identity.Accounts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IGCSELearningHub.WebAPI.Controllers.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account-profiles")]
    [Authorize]
    public class AccountProfilesController : ControllerBase
    {
        private readonly IAccountProfileService _profileService;
        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public AccountProfilesController(IAccountProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateAccountDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _profileService.UpdateProfileAsync(CurrentUserId, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _profileService.ResetPasswordAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var origin = Request.Headers.Origin.FirstOrDefault()
                         ?? $"{Request.Scheme}://{Request.Host}";
            var result = await _profileService.SendPasswordResetEmailAsync(dto, origin, ct);
            return StatusCode(result.StatusCode, result);
        }
    }
}
