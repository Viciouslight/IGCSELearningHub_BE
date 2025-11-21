using Asp.Versioning;
using IGCSELearningHub.Application.Orders.Billing.Reconciliation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGCSELearningHub.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/billing/reconciliation")]
    [Authorize(Roles = "Admin")]
    public class AdminBillingReconciliationController : ControllerBase
    {
        private readonly IReconciliationService _reconciliation;

        public AdminBillingReconciliationController(IReconciliationService reconciliation)
        {
            _reconciliation = reconciliation;
        }

        [HttpGet]
        public async Task<IActionResult> Reconcile([FromQuery] int? orderId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 100)
        {
            var result = await _reconciliation.ReconcileAsync(orderId, page, pageSize);
            return Ok(result);
        }
    }
}
