namespace IGCSELearningHub.Application.Orders.Billing.Reconciliation.DTOs
{
    public class ReconciliationItemDTO
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal PaymentsTotal { get; set; }
        public decimal InvoicesTotal { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<string> Issues { get; set; } = new();
    }

    public class ReconciliationResultDTO
    {
        public List<ReconciliationItemDTO> Items { get; set; } = new();
        public int TotalOrdersChecked { get; set; }
        public int OrdersWithIssues { get; set; }
    }
}
