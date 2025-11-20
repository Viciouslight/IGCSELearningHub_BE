using IGCSELearningHub.Domain.Orders.Enums;

namespace IGCSELearningHub.Application.Orders.Billing.DTOs
{
    public class InvoiceListQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public InvoiceStatus? Status { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? OrderId { get; set; }
    }
}
