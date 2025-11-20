using IGCSELearningHub.Domain.Common;
using IGCSELearningHub.Domain.Orders.Enums;

namespace IGCSELearningHub.Domain.Orders.Entities;

public class Invoice : BaseFullEntity
{
    public int OrderId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal Total { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Issued;

    public string? BuyerName { get; set; }

    public string? BuyerTaxCode { get; set; }

    public string? BuyerAddress { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
}
