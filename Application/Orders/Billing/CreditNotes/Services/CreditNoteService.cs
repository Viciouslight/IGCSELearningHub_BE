using IGCSELearningHub.Application.Orders.Billing.CreditNotes.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing;
using IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.Shared.DTOs;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;

namespace IGCSELearningHub.Application.Orders.Billing.CreditNotes.Services
{
    public class CreditNoteService : ICreditNoteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceNumberGenerator _numberGenerator;
        private readonly IInvoiceValidationService _validation;

        public CreditNoteService(IUnitOfWork uow, IInvoiceNumberGenerator numberGenerator, IInvoiceValidationService validation)
        {
            _uow = uow;
            _numberGenerator = numberGenerator;
            _validation = validation;
        }

        public async Task<ApiResult<InvoiceDTO>> IssueCreditNoteAsync(int invoiceId, string reason = "")
        {
            var validation = await _validation.ValidateInvoiceForCreditNoteAsync(invoiceId);
            if (!validation.Succeeded) return ApiResult<InvoiceDTO>.Fail(validation.Message ?? "Validation failed.", validation.StatusCode);
            var source = validation.Data!;

            var credit = new Invoice
            {
                OrderId = source.OrderId,
                InvoiceNumber = await _numberGenerator.GenerateAsync(),
                IssueDate = DateTime.UtcNow,
                Subtotal = -source.Subtotal,
                TaxAmount = -source.TaxAmount,
                Total = -source.Total,
                BuyerName = source.BuyerName,
                BuyerTaxCode = source.BuyerTaxCode,
                BuyerAddress = source.BuyerAddress,
                Status = InvoiceStatus.Issued,
                Notes = $"Credit note for {source.InvoiceNumber}. {reason}".Trim(),
                IsCreditNote = true,
                ReferenceInvoiceNumber = source.InvoiceNumber
            };

            await _uow.InvoiceRepository.AddAsync(credit);
            await _uow.SaveChangesAsync();

            foreach (var line in source.Lines.Where(l => !l.IsDeleted))
            {
                var creditLine = new InvoiceLine
                {
                    InvoiceId = credit.Id,
                    ItemType = line.ItemType,
                    ItemId = line.ItemId,
                    Description = $"Credit for {line.Description}",
                    Quantity = -line.Quantity,
                    UnitPrice = line.UnitPrice,
                    TaxRate = line.TaxRate,
                    LineTotal = -line.LineTotal
                };
                await _uow.InvoiceLineRepository.AddAsync(creditLine);
            }

            await _uow.SaveChangesAsync();
            return ApiResult<InvoiceDTO>.Success(InvoiceMapper.Map(credit), "Credit note issued.");
        }
    }
}
