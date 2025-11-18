using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repositories
{
    public class QuestionOptionRepository : GenericRepository<QuestionOption>, IQuestionOptionRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionOptionRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

