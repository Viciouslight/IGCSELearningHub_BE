using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repositories
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public AssignmentRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

