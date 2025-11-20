using IGCSELearningHub.Application.IRepositories;
using IGCSELearningHub.Application.Utils.Interfaces;
using IGCSELearningHub.Domain.Payments.Entities;
using IGCSELearningHub.Infrastructure.Repositories;
using Infrastructure.Data;

namespace IGCSELearningHub.Infrastructure.Payments.Repositories
{
    public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethodRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentMethodRepository(AppDbContext dbContext, IDateTimeProvider clock) : base(dbContext, clock)
        {
            _appDbContext = dbContext;
        }
    }
}

