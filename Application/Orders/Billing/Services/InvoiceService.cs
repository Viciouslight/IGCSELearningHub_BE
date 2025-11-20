using IGCSELearningHub.Application.Orders.Invoices.DTOs;
using IGCSELearningHub.Application.Orders.Invoices.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderPricing.Interfaces;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Orders.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Invoices.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderPricingService _pricing;

        public InvoiceService(IUnitOfWork uow, IOrderPricingService pricing)
        {
            _uow = uow;
            _pricing = pricing;
        }

        public async Task<ApiResult<InvoiceDTO>> IssueInvoiceAsync(int orderId, string buyerName = "", string? buyerTaxCode = null, string? buyerAddress = null)
        {
            var order = await _uow.OrderRepository.GetAllQueryable($"{nameof(Order.OrderDetails)}")
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return ApiResult<InvoiceDTO>.Fail("OrderIndex not found.", 404);

            // Avoid duplicate invoice
            var existing = await _uow.InvoiceRepository.GetAllQueryable()
                .FirstOrDefaultAsync(i => i.OrderId == orderId && i.Status != InvoiceStatus.Voided);
            if (existing != null) return ApiResult<InvoiceDTO>.Success(Map(existing), "Invoice already exists.");

            // Price from order details (assumes OrderDetails already priced)
            var subtotal = order.OrderDetails.Where(d => !d.IsDeleted).Sum(d => d.Price);
            var taxAmount = 0m; // no tax calc for now
            var total = subtotal + taxAmount;

            var invoice = new Invoice
            {
                OrderId = order.Id,
                InvoiceNumber = await GenerateInvoiceNumberAsync(),
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

            return ApiResult<InvoiceDTO>.Success(Map(invoice), "Invoice issued.");
        }

        public async Task<ApiResult<InvoiceDTO>> GetInvoiceAsync(int invoiceId)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null) return ApiResult<InvoiceDTO>.Fail("Invoice not found.", 404);
            return ApiResult<InvoiceDTO>.Success(Map(invoice));
        }

        public async Task<ApiResult<InvoiceDTO>> GetInvoiceByNumberAsync(string invoiceNumber)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
            if (invoice == null) return ApiResult<InvoiceDTO>.Fail("Invoice not found.", 404);
            return ApiResult<InvoiceDTO>.Success(Map(invoice));
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

        private async Task<string> GenerateInvoiceNumberAsync()
        {
            // simple timestamp-based number; replace with sequence if needed
            string number;
            do
            {
                number = $"INV-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            } while (await _uow.InvoiceRepository.GetByInvoiceNumberAsync(number) != null);

            return number;
        }

        private static InvoiceDTO Map(Invoice invoice)
        {
            return new InvoiceDTO
            {
                InvoiceId = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                Status = invoice.Status,
                Subtotal = invoice.Subtotal,
                TaxAmount = invoice.TaxAmount,
                Total = invoice.Total,
                BuyerName = invoice.BuyerName,
                BuyerTaxCode = invoice.BuyerTaxCode,
                BuyerAddress = invoice.BuyerAddress,
                Lines = invoice.Lines.Select(l => new InvoiceLineDTO
                {
                    InvoiceLineId = l.Id,
                    ItemType = l.ItemType,
                    ItemId = l.ItemId,
                    Description = l.Description,
                    Quantity = l.Quantity,
                    UnitPrice = l.UnitPrice,
                    TaxRate = l.TaxRate,
                    LineTotal = l.LineTotal
                }).ToList()
            };
        }
    }
}
