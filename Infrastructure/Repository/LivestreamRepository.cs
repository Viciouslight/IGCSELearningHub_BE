using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repository
{
    public class LivestreamRepository : GenericRepository<Livestream>, ILivestreamRepository
    {
        private readonly AppDbContext _appDbContext;
        public LivestreamRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

