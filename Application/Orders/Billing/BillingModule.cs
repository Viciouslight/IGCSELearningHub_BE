using IGCSELearningHub.Application.Orders.Billing.CreditNotes.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.CreditNotes.Services;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceIssuing.Services;
using IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceNumbering.Services;
using IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceQueries.Services;
using IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.InvoiceValidation.Services;
using IGCSELearningHub.Application.Orders.Billing.Reconciliation.Interfaces;
using IGCSELearningHub.Application.Orders.Billing.Reconciliation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Billing;

public static class BillingModule
{
    public static IServiceCollection AddBillingModule(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceIssuingService, InvoiceIssuingService>();
        services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();
        services.AddScoped<IInvoiceNumberGenerator, InvoiceNumberGenerator>();
        services.AddScoped<IInvoiceValidationService, InvoiceValidationService>();
        services.AddScoped<ICreditNoteService, CreditNoteService>();
        services.AddScoped<IReconciliationService, ReconciliationService>();
        return services;
    }
}
