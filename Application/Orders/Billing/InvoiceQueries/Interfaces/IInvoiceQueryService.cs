using IGCSELearningHub.Application.Orders.Billing.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Interfaces
{
    public interface IInvoiceQueryService
    {
        Task<ApiResult<InvoiceDTO>> GetInvoiceAsync(int invoiceId);
        Task<ApiResult<InvoiceDTO>> GetInvoiceByNumberAsync(string invoiceNumber);
        Task<PagedResult<InvoiceDTO>> ListInvoicesAsync(InvoiceListQuery query);
    }
}
