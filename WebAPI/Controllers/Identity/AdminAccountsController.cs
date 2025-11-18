using Asp.Versioning;
using IGCSELearningHub.Application.Exceptions;
using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Identity.Accounts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGCSELearningHub.WebAPI.Controllers.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    [Authorize]
    public class AdminAccountsController : ControllerBase
    {
        private readonly IAccountAdminService _accountService;
        private readonly IAccountProfileService _profileService;

        public AdminAccountsController(IAccountAdminService accountService, IAccountProfileService profileService)
        {
            _accountService = accountService;
            _profileService = profileService;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAccountsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged(
            [FromQuery] string? q,
            [FromQuery] string? role,
            [FromQuery] string? status,
            [FromQuery] string? sort,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _accountService.GetAccountsPagedAsync(q, role, status, pageNumber, pageSize, sort);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("check-exists")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckUsernameOrEmailExists([FromQuery] string username, [FromQuery] string email)
        {
            var result = await _accountService.CheckUsernameOrEmailExistsAsync(username, email);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid");
                throw new ValidationException(errors);
            }

            var result = await _profileService.UpdateProfileAsync(id, updateDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("ban/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> BanAccount(int id)
        {
            var result = await _accountService.BanAccountAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("unban/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UnbanAccount(int id)
        {
            var result = await _accountService.UnbanAccountAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
