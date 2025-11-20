using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Infrastructure.Repositories;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Orders.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

