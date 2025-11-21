using IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Services
{
    public class InvoiceValidationService : IInvoiceValidationService
    {
        private readonly IUnitOfWork _uow;

        public InvoiceValidationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ApiResult<Order>> ValidateOrderForInvoiceAsync(int orderId)
        {
            var order = await _uow.OrderRepository.GetAllQueryable($"{nameof(Order.OrderDetails)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return ApiResult<Order>.Fail("Order not found.", 404);

            if (order.Status != OrderStatus.Paid)
                return ApiResult<Order>.Fail("Order must be paid before invoicing.", 400);

            var existing = await _uow.InvoiceRepository.GetAllQueryable()
                .AnyAsync(i => i.OrderId == orderId && i.Status != InvoiceStatus.Voided);
            if (existing)
                return ApiResult<Order>.Fail("Invoice already exists for this order.", 400);

            return ApiResult<Order>.Success(order);
        }

        public async Task<ApiResult<Invoice>> ValidateInvoiceForCreditNoteAsync(int invoiceId)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null) return ApiResult<Invoice>.Fail("Invoice not found.", 404);

            if (invoice.IsCreditNote)
                return ApiResult<Invoice>.Fail("Cannot issue credit note from another credit note.", 400);

            if (invoice.Status != InvoiceStatus.Issued)
                return ApiResult<Invoice>.Fail("Only issued invoices can receive a credit note.", 400);

            return ApiResult<Invoice>.Success(invoice);
        }

        public async Task<ApiResult<Invoice>> ValidateInvoiceForVoidAsync(int invoiceId)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null) return ApiResult<Invoice>.Fail("Invoice not found.", 404);

            if (invoice.Status == InvoiceStatus.Voided)
                return ApiResult<Invoice>.Fail("Invoice already voided.", 400);

            return ApiResult<Invoice>.Success(invoice);
        }
    }
}
