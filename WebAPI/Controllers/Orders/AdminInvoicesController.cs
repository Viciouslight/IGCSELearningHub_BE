using Asp.Versioning;
using IGCSELearningHub.Application.Orders.Billing.CreditNotes.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.DTOs;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGCSELearningHub.WebAPI.Controllers.Orders
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/invoices")]
    [Authorize(Roles = "Admin")]
    public class AdminInvoicesController : ControllerBase
    {
        private readonly IInvoiceIssuingService _issuing;
        private readonly IInvoiceQueryService _queries;
        private readonly ICreditNoteService _creditNotes;

        public AdminInvoicesController(
            IInvoiceIssuingService issuing,
            IInvoiceQueryService queries,
            ICreditNoteService creditNotes)
        {
            _issuing = issuing;
            _queries = queries;
            _creditNotes = creditNotes;
        }

        [HttpPost]
        public async Task<IActionResult> Issue([FromBody] IssueInvoiceRequest request)
        {
            var result = await _issuing.IssueInvoiceAsync(request.OrderId, request.BuyerName, request.BuyerTaxCode, request.BuyerAddress);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{invoiceId:int}/credit-note")]
        public async Task<IActionResult> CreditNote([FromRoute] int invoiceId, [FromBody] VoidInvoiceRequest? request = null)
        {
            var result = await _creditNotes.IssueCreditNoteAsync(invoiceId, request?.Reason ?? "");
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{invoiceId:int}/void")]
        public async Task<IActionResult> Void([FromRoute] int invoiceId, [FromBody] VoidInvoiceRequest? request = null)
        {
            var result = await _issuing.VoidInvoiceAsync(invoiceId, request?.Reason ?? "");
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{invoiceId:int}")]
        public async Task<IActionResult> Get([FromRoute] int invoiceId)
        {
            var result = await _queries.GetInvoiceAsync(invoiceId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-number/{invoiceNumber}")]
        public async Task<IActionResult> GetByNumber([FromRoute] string invoiceNumber)
        {
            var result = await _queries.GetInvoiceByNumberAsync(invoiceNumber);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] InvoiceListQuery query)
        {
            var result = await _queries.ListInvoicesAsync(query);
            return StatusCode(result.StatusCode, result);
        }
    }
}
