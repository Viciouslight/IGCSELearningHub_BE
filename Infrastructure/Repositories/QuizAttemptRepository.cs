using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repositories
{
    public class QuizAttemptRepository : GenericRepository<QuizAttempt>, IQuizAttemptRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuizAttemptRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

