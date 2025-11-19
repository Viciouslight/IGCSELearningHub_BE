using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Payments.Entities;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

