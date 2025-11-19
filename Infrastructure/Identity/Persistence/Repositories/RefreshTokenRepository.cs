using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Identity.Entities;
using IGCSELearningHub.Infrastructure.Repositories;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Identity.Persistence.Repositories
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
