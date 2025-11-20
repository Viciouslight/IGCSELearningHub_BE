using IGCSELearningHub.Application.Orders.Billing.Reconciliation.DTOs;

namespace IGCSELearningHub.Application.Orders.Billing.Reconciliation.Interfaces
{
    public interface IReconciliationService
    {
        Task<ReconciliationResultDTO> ReconcileAsync(int? orderId = null, int page = 1, int pageSize = 100);
    }
}
