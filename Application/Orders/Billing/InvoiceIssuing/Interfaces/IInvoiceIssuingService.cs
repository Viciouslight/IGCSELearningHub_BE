using IGCSELearningHub.Application.Orders.Billing.Shared.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Interfaces
{
    public interface IInvoiceIssuingService
    {
        Task<ApiResult<InvoiceDTO>> IssueInvoiceAsync(int orderId, string buyerName = "", string? buyerTaxCode = null, string? buyerAddress = null);
        Task<ApiResult<InvoiceDTO>> VoidInvoiceAsync(int invoiceId, string reason = "");
    }
}
