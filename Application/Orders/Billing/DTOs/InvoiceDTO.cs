using IGCSELearningHub.Domain.Enums;
using IGCSELearningHub.Domain.Orders.Enums;

namespace IGCSELearningHub.Application.Orders.Invoices.DTOs
{
    public class InvoiceLineDTO
    {
        public int InvoiceLineId { get; set; }
        public ItemType ItemType { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public string? BuyerName { get; set; }
        public string? BuyerTaxCode { get; set; }
        public string? BuyerAddress { get; set; }
        public List<InvoiceLineDTO> Lines { get; set; } = new();
    }
}
