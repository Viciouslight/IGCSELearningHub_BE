using IGCSELearningHub.Domain.Identity.Entities;
using System.Linq.Expressions;

namespace IGCSELearningHub.Application.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetByUsernameOrEmail(string email, string Username);
        Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate);
    }
}
