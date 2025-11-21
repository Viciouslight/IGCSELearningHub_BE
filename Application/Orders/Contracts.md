### Ordering contracts (public for other modules)
- `Ordering/OrderPlacement/Interfaces/IOrderService` — create order, get order, list my orders.
- `Ordering/OrderQueries/Interfaces/IOrderQueryService` — order status, queries.
- `Ordering/OrderLifecycle/Interfaces/IOrderLifecycleService` — mark paid/failed/cancel/expire (idempotent state transitions).
- `Ordering/OrderPricing/Interfaces/IOrderPricingService` — build price quotes.
- `Ordering/OrderValidation/Interfaces/IOrderValidationService` — validate items/ownership before pricing/placement.

### Billing contracts (public)
- `Billing/InvoiceIssuing/Interfaces/IInvoiceIssuingService` — issue/void invoice.
- `Billing/InvoiceQueries/Interfaces/IInvoiceQueryService` — list/filter/detail invoice.
- `Billing/CreditNotes/Interfaces/ICreditNoteService` — issue credit notes linked to original invoices.
- `Billing/InvoiceNumbering/Interfaces/IInvoiceNumberGenerator` — policy for unique invoice numbers.
- `Billing/InvoiceValidation/Interfaces/IInvoiceValidationService` — guards for issue/void/credit.
- `Billing/Reconciliation/Interfaces/IReconciliationService` — reconcile Order ↔ Payments ↔ Invoices.

### Usage notes
- Modules outside Orders/Billing should depend only on the interfaces above (inject via DI), never on repositories.
- Payment callbacks: call `IOrderLifecycleService` (state) and optionally issue invoices via `IInvoiceIssuingService` after paid.
- Enrollment automation: hook into `IOrderLifecycleService` paid transition (event/handler) instead of repositories.
