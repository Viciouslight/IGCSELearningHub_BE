namespace IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Interfaces
{
    public interface IInvoiceNumberGenerator
    {
        Task<string> GenerateAsync();
    }
}
