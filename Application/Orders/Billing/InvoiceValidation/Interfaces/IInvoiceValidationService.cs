using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Interfaces
{
    public interface IInvoiceValidationService
    {
        Task<ApiResult<Order>> ValidateOrderForInvoiceAsync(int orderId);
        Task<ApiResult<Invoice>> ValidateInvoiceForCreditNoteAsync(int invoiceId);
        Task<ApiResult<Invoice>> ValidateInvoiceForVoidAsync(int invoiceId);
    }
}
