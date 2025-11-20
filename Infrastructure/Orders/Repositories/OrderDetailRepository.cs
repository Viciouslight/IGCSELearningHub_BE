using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Infrastructure.Repositories;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Orders.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderDetailRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

