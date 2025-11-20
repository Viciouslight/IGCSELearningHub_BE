using IGCSELearningHub.Application.Orders.Billing.DTOs;
using IGCSELearningHub.Application.Wrappers;

namespace IGCSELearningHub.Application.Orders.Billing.CreditNotes.Interfaces
{
    public interface ICreditNoteService
    {
        Task<ApiResult<InvoiceDTO>> IssueCreditNoteAsync(int invoiceId, string reason = "");
    }
}
