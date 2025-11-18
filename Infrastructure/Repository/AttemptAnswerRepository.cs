using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repository
{
    public class AttemptAnswerRepository : GenericRepository<AttemptAnswer>, IAttemptAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public AttemptAnswerRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

