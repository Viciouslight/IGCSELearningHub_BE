using AutoMapper;
using IGCSELearningHub.Application.Extensions;
using IGCSELearningHub.Application.Identity.Accounts.DTOs;
using IGCSELearningHub.Application.Identity.Accounts.Interfaces;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Identity.Accounts.Services
{
    public class AccountAdminService : IAccountAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountAdminService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (account == null) return ApiResult<AccountDTO>.Fail("Account not found.", 404);

            return ApiResult<AccountDTO>.Success(_mapper.Map<AccountDTO>(account), "Account retrieved successfully.");
        }

        public async Task<PagedResult<AccountDTO>> GetAccountsPagedAsync(string? q, string? role, string? status, int page, int pageSize, string? sort)
        {
            var query = _unitOfWork.AccountRepository.GetAllQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                var key = q.Trim().ToLower();
                query = query.Where(a =>
                    a.UserName.ToLower().Contains(key) ||
                    a.Email.ToLower().Contains(key) ||
                    (a.FullName != null && a.FullName.ToLower().Contains(key)));
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                var r = role.Trim();
                query = query.Where(a => a.Role.ToString() == r);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                var s = status.Trim().ToLower();
                query = query.Where(a => (a.Status ?? "").ToLower() == s);
            }

            query = (sort ?? "").ToLower() switch
            {
                "createdat_asc" => query.OrderBy(a => a.CreatedAt),
                "name_asc" => query.OrderBy(a => a.UserName),
                "name_desc" => query.OrderByDescending(a => a.UserName),
                "email_asc" => query.OrderBy(a => a.Email),
                "email_desc" => query.OrderByDescending(a => a.Email),
                _ => query.OrderByDescending(a => a.CreatedAt)
            };

            return await query.ToPagedResultAsync(page, pageSize, a => _mapper.Map<AccountDTO>(a));
        }

        public async Task<ApiResult<bool>> CheckUsernameOrEmailExistsAsync(string username, string email)
        {
            var existedAccount = await _unitOfWork.AccountRepository.GetByUsernameOrEmail(email, username);
            return ApiResult<bool>.Success(existedAccount != null,
                existedAccount != null ? "Username or Email already exists." : "Username or Email does not exist.");
        }

        public async Task<ApiResult<string>> BanAccountAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return ApiResult<string>.Fail("Account not found.", 404);

            account.Status = "Banned";
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult<string>.Success("Account banned successfully.");
        }

        public async Task<ApiResult<string>> UnbanAccountAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            if (account == null) return ApiResult<string>.Fail("Account not found.", 404);

            account.Status = "Active";
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult<string>.Success("Account unbanned successfully.");
        }
    }
}
