using IGCSELearningHub.Domain.Identity.Accounts;
using System.Linq.Expressions;

namespace IGCSELearningHub.Application.IRepository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetByUsernameOrEmail(string email, string Username);
        Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate);
    }
}
