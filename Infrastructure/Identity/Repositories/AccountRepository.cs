using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Identity.Entities;
using IGCSELearningHub.Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IGCSELearningHub.Infrastructure.Identity.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
        public AccountRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }

        public async Task<Account> GetByUsernameOrEmail(string email, string username)
        {
            var account = await _appDbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email || x.UserName == username);

            return account;
        }

        public async Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }
    }
}
