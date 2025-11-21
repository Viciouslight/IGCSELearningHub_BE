using IGCSELearningHub.Application.Orders.Billing.Shared.DTOs;
using IGCSELearningHub.Domain.Orders.Entities;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing
{
    public static class InvoiceMapper
    {
        public static InvoiceDTO Map(Invoice invoice)
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
                IsCreditNote = invoice.IsCreditNote,
                ReferenceInvoiceNumber = invoice.ReferenceInvoiceNumber,
                Notes = invoice.Notes,
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
