using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.Shared.DTOs;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Services
{
    public class InvoiceIssuingService : IInvoiceIssuingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceNumberGenerator _numberGenerator;
        private readonly IInvoiceValidationService _validation;

        public InvoiceIssuingService(IUnitOfWork uow, IInvoiceNumberGenerator numberGenerator, IInvoiceValidationService validation)
        {
            _uow = uow;
            _numberGenerator = numberGenerator;
            _validation = validation;
        }

        public async Task<ApiResult<InvoiceDTO>> IssueInvoiceAsync(int orderId, string buyerName = "", string? buyerTaxCode = null, string? buyerAddress = null)
        {
            var validation = await _validation.ValidateOrderForInvoiceAsync(orderId);
            if (!validation.Succeeded) return ApiResult<InvoiceDTO>.Fail(validation.Message ?? "Validation failed.", validation.StatusCode);
            var order = validation.Data!;

            var subtotal = order.OrderDetails.Where(d => !d.IsDeleted).Sum(d => d.Price);
            var taxAmount = 0m; // extend when tax rules available
            var total = subtotal + taxAmount;

            var invoice = new Invoice
            {
                OrderId = order.Id,
                InvoiceNumber = await _numberGenerator.GenerateAsync(),
                IssueDate = DateTime.UtcNow,
                Subtotal = subtotal,
                TaxAmount = taxAmount,
                Total = total,
                BuyerName = buyerName,
                BuyerTaxCode = buyerTaxCode,
                BuyerAddress = buyerAddress,
                Status = InvoiceStatus.Issued
            };
            await _uow.InvoiceRepository.AddAsync(invoice);
            await _uow.SaveChangesAsync();

            foreach (var line in order.OrderDetails.Where(d => !d.IsDeleted))
            {
                var invLine = new InvoiceLine
                {
                    InvoiceId = invoice.Id,
                    ItemType = line.ItemType,
                    ItemId = line.ItemId,
                    Description = await BuildDescriptionAsync(line.ItemType, line.ItemId),
                    Quantity = 1,
                    UnitPrice = line.Price,
                    TaxRate = 0m,
                    LineTotal = line.Price
                };
                await _uow.InvoiceLineRepository.AddAsync(invLine);
            }

            await _uow.SaveChangesAsync();

            return ApiResult<InvoiceDTO>.Success(InvoiceMapper.Map(invoice), "Invoice issued.");
        }

        public async Task<ApiResult<InvoiceDTO>> VoidInvoiceAsync(int invoiceId, string reason = "")
        {
            var validation = await _validation.ValidateInvoiceForVoidAsync(invoiceId);
            if (!validation.Succeeded) return ApiResult<InvoiceDTO>.Fail(validation.Message ?? "Validation failed.", validation.StatusCode);
            var invoice = validation.Data!;

            invoice.Status = InvoiceStatus.Voided;
            invoice.Notes = string.IsNullOrWhiteSpace(reason) ? invoice.Notes : reason;
            _uow.InvoiceRepository.Update(invoice);
            await _uow.SaveChangesAsync();

            return ApiResult<InvoiceDTO>.Success(InvoiceMapper.Map(invoice), "Invoice voided.");
        }

        private async Task<string> BuildDescriptionAsync(ItemType type, int itemId)
        {
            return type switch
            {
                ItemType.Course => (await _uow.CourseRepository.GetByIdAsync(itemId))?.Title ?? $"Course #{itemId}",
                ItemType.CoursePackage => (await _uow.CoursePackageRepository.GetByIdAsync(itemId))?.Name ?? $"Package #{itemId}",
                ItemType.Livestream => (await _uow.LivestreamRepository.GetByIdAsync(itemId))?.Title ?? $"Livestream #{itemId}",
                _ => $"Item #{itemId}"
            };
        }
    }
}
