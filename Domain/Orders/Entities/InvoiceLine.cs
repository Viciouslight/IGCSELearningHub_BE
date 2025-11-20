using IGCSELearningHub.Domain.Common;
using IGCSELearningHub.Domain.Enums;

namespace IGCSELearningHub.Domain.Orders.Entities;

public class InvoiceLine : BaseFullEntity
{
    public int InvoiceId { get; set; }

    public ItemType ItemType { get; set; }

    public int ItemId { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; } = 1;

    public decimal UnitPrice { get; set; }

    public decimal TaxRate { get; set; }

    public decimal LineTotal { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
