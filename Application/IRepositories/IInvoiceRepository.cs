using IGCSELearningHub.Domain.Orders.Entities;

namespace IGCSELearningHub.Application.IRepositories
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber);
    }
}
