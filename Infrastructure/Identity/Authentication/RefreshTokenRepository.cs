using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Identity.Authentication;
using IGCSELearningHub.Infrastructure.Repository;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Identity.Authentication
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _db;
        public RefreshTokenRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _db = dbContext;
        }
    }
}
