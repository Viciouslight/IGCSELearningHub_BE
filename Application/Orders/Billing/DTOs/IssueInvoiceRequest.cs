namespace IGCSELearningHub.Application.Orders.Billing.DTOs
{
    public class IssueInvoiceRequest
    {
        public int OrderId { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string? BuyerTaxCode { get; set; }
        public string? BuyerAddress { get; set; }
    }
}
