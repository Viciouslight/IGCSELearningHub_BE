namespace IGCSELearningHub.Application.Orders.Billing.Reconciliation.DTOs
{
    public class ReconciliationResultDTO
    {
        public List<ReconciliationItemDTO> Items { get; set; } = new();
        public int TotalOrdersChecked { get; set; }
        public int OrdersWithIssues { get; set; }
    }
}
