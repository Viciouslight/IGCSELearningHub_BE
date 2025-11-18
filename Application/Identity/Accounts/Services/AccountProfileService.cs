using AutoMapper;
using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Identity.Accounts.Interfaces;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using IGCSELearningHub.Application.Identity.Authentication.Interfaces;

namespace IGCSELearningHub.Application.Identity.Accounts.Services
{
    public class AccountProfileService : IAccountProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountProfileService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public AccountProfileService(
            IUnitOfWork unitOfWork,
            ILogger<AccountProfileService> logger,
            IEmailSender emailSender,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResult<AccountDTO>> UpdateProfileAsync(int accountId, UpdateAccountDTO updateDto)
        {
            var existedAccount = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(updateDto.Email, updateDto.UserName);

            if (existedAccount != null && existedAccount.Id != accountId)
            {
                return ApiResult<AccountDTO>.Fail("User already exists with the provided email or username.", 400);
            }

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return ApiResult<AccountDTO>.Fail("Account not found.", 404);

            account.FullName = updateDto.FullName;
            account.PhoneNumber = updateDto.PhoneNumber;
            account.UserName = updateDto.UserName;
            account.Email = updateDto.Email;
            account.Gender = updateDto.Gender;

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult<AccountDTO>.Success(_mapper.Map<AccountDTO>(account), "Profile updated successfully.");
        }

        public async Task<ApiResult<string>> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(dto.UserNameOrEmail, dto.UserNameOrEmail);
            if (account == null)
                return ApiResult<string>.Fail("Account with the given email or username does not exist.", 400);

            if (account.IsExternal && !string.IsNullOrWhiteSpace(account.ExternalProvider))
                return ApiResult<string>.Fail("Tài khoản này chỉ có thể đăng nhập bằng Google.", 400);

            if (dto.NewPassword != dto.ConfirmNewPassword)
                return ApiResult<string>.Fail("New password and confirmation do not match.", 400);

            account.Password = _passwordHasher.HashPassword(dto.NewPassword);

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult<string>.Success("Password reset successfully.");
        }

        public async Task<ApiResult<string>> SendPasswordResetEmailAsync(ForgotPasswordRequestDTO dto, string origin, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return ApiResult<string>.Fail("Email is required.", 400);

            var account = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(dto.Email, dto.Email);
            if (account == null)
            {
                return ApiResult<string>.Success(GenericPasswordResetResponse);
            }

            if (account.IsExternal && !string.IsNullOrWhiteSpace(account.ExternalProvider))
            {
                return ApiResult<string>.Fail("Tài khoản này chỉ có thể đăng nhập bằng Google.", 400);
            }

            var originalPassword = account.Password;
            var temporaryPassword = GenerateTemporaryPassword();
            account.Password = _passwordHasher.HashPassword(temporaryPassword);

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            var subject = "IGCSE Learning Hub - Password Reset";
            var body = BuildResetEmailBody(account.FullName, account.UserName, temporaryPassword, BuildResetUrl(origin));

            try
            {
                await _emailSender.SendAsync(account.Email, subject, body, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset email to {Email}", account.Email);
                account.Password = originalPassword;
                _unitOfWork.AccountRepository.Update(account);
                await _unitOfWork.SaveChangesAsync();
                return ApiResult<string>.Fail("Failed to send password reset email. Please try again later.", 500);
            }

            return ApiResult<string>.Success(GenericPasswordResetResponse);
        }

        private const string GenericPasswordResetResponse = "If an account exists for the provided email, a reset email has been sent.";

        private static string GenerateTemporaryPassword(int length = 12)
        {
            const string allowed = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789@$!%*?&";
            var bytes = RandomNumberGenerator.GetBytes(length);
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowed[bytes[i] % allowed.Length];
            }
            return new string(chars);
        }

        private static string BuildResetUrl(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin)) return string.Empty;
            return $"{origin.TrimEnd('/')}/reset-password";
        }

        private static string BuildResetEmailBody(string? fullName, string userName, string tempPassword, string resetUrl)
        {
            var displayName = string.IsNullOrWhiteSpace(fullName) ? userName : fullName;
            var sb = new StringBuilder();
            sb.AppendLine($"<p>Hi {displayName},</p>");
            sb.AppendLine("<p>We received a request to reset your password. Use the temporary password below to sign in and change it immediately:</p>");
            sb.AppendLine($"<p><strong>Temporary password:</strong> {tempPassword}</p>");
            sb.AppendLine("<p>After signing in, please update your password from your profile page.</p>");
            if (!string.IsNullOrWhiteSpace(resetUrl))
            {
                sb.AppendLine($"<p>You can access the reset page here: <a href=\"{resetUrl}\">{resetUrl}</a></p>");
            }
            sb.AppendLine("<p>If you didn’t request a reset, you can ignore this email. Your password will remain unchanged, but we recommend updating it if you have any concerns.</p>");
            sb.AppendLine("<p>Best regards,<br/>IGCSE Learning Hub Team</p>");
            return sb.ToString();
        }
    }
}
