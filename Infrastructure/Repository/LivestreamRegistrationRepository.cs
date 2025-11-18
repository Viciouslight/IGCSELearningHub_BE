using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repository
{
    public class LivestreamRegistrationRepository : GenericRepository<LivestreamRegistration>, ILivestreamRegistrationRepository
    {
        private readonly AppDbContext _appDbContext;
        public LivestreamRegistrationRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

