using IGCSELearningHub.Application.Extensions;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing;
using IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.DTOs;
using IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.Shared.DTOs;
using IGCSELearningHub.Application.Wrappers;
using IGCSELearningHub.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Services
{
    public class InvoiceQueryService : IInvoiceQueryService
    {
        private readonly IUnitOfWork _uow;

        public InvoiceQueryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ApiResult<InvoiceDTO>> GetInvoiceAsync(int invoiceId)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice == null) return ApiResult<InvoiceDTO>.Fail("Invoice not found.", 404);
            return ApiResult<InvoiceDTO>.Success(InvoiceMapper.Map(invoice));
        }

        public async Task<ApiResult<InvoiceDTO>> GetInvoiceByNumberAsync(string invoiceNumber)
        {
            var invoice = await _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}")
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
            if (invoice == null) return ApiResult<InvoiceDTO>.Fail("Invoice not found.", 404);
            return ApiResult<InvoiceDTO>.Success(InvoiceMapper.Map(invoice));
        }

        public async Task<PagedResult<InvoiceDTO>> ListInvoicesAsync(InvoiceListQuery query)
        {
            var q = _uow.InvoiceRepository.GetAllQueryable($"{nameof(Invoice.Lines)}");

            if (query.Status.HasValue)
                q = q.Where(i => i.Status == query.Status.Value);
            if (!string.IsNullOrWhiteSpace(query.InvoiceNumber))
                q = q.Where(i => i.InvoiceNumber.Contains(query.InvoiceNumber));
            if (query.OrderId.HasValue)
                q = q.Where(i => i.OrderId == query.OrderId.Value);

            q = q.OrderByDescending(i => i.IssueDate);

            return await q.ToPagedResultAsync(query.PageNumber, query.PageSize, i => InvoiceMapper.Map(i));
        }
    }
}
