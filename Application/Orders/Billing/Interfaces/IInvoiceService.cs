using IGCSELearningHub.Application.Orders.Invoices.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Invoices.Interfaces
{
    public interface IInvoiceService
    {
        Task<ApiResult<InvoiceDTO>> IssueInvoiceAsync(int orderId, string buyerName = "", string? buyerTaxCode = null, string? buyerAddress = null);
        Task<ApiResult<InvoiceDTO>> GetInvoiceAsync(int invoiceId);
        Task<ApiResult<InvoiceDTO>> GetInvoiceByNumberAsync(string invoiceNumber);
    }
}
