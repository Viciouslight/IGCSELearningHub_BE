using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repositories
{
    public class ProgressRepository : GenericRepository<Progress>, IProgressRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProgressRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

