using IGCSELearningHub.Application.Orders.Billing.Reconciliation.DTOs;
using IGCSELearningHub.Application.Orders.Billing.Reconciliation.Interfaces;
using IGCSELearningHub.Domain.Orders.Entities;
using IGCSELearningHub.Domain.Payments.Enums;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Billing.Reconciliation.Services
{
    public class ReconciliationService : IReconciliationService
    {
        private readonly IUnitOfWork _uow;

        public ReconciliationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ReconciliationResultDTO> ReconcileAsync(int? orderId = null, int page = 1, int pageSize = 100)
        {
            var q = _uow.OrderRepository.GetAllQueryable($"{nameof(Order.Payments)},{nameof(Order.Invoices)},{nameof(Order.OrderDetails)}");
            if (orderId.HasValue) q = q.Where(o => o.Id == orderId.Value);
            q = q.OrderByDescending(o => o.Id);

            var batch = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new ReconciliationResultDTO
            {
                TotalOrdersChecked = batch.Count
            };

            foreach (var order in batch)
            {
                var paymentsTotal = order.Payments
                    .Where(p => !p.IsDeleted && p.Status == PaymentStatus.Paid)
                    .Sum(p => p.Amount);

                var invoicesTotal = order.Invoices
                    .Where(i => !i.IsDeleted && i.Status != Domain.Orders.Enums.InvoiceStatus.Voided)
                    .Sum(i => i.Total);

                var issues = new List<string>();
                if (order.TotalAmount != paymentsTotal) issues.Add($"OrderTotal {order.TotalAmount} != Payments {paymentsTotal}");
                if (order.TotalAmount != invoicesTotal) issues.Add($"OrderTotal {order.TotalAmount} != Invoices {invoicesTotal}");
                if (paymentsTotal != invoicesTotal) issues.Add($"Payments {paymentsTotal} != Invoices {invoicesTotal}");

                result.Items.Add(new ReconciliationItemDTO
                {
                    OrderId = order.Id,
                    OrderTotal = order.TotalAmount,
                    PaymentsTotal = paymentsTotal,
                    InvoicesTotal = invoicesTotal,
                    Status = order.Status.ToString(),
                    Issues = issues
                });

                if (issues.Count > 0) result.OrdersWithIssues++;
            }

            return result;
        }
    }
}
