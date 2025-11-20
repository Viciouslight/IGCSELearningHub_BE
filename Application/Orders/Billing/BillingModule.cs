using IGCSELearningHub.Application.Orders.Invoices.Interfaces;
using IGCSELearningHub.Application.Orders.Invoices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Invoices;

public static class BillingModule
{
    public static IServiceCollection AddBillingModule(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceService, InvoiceService>();
        return services;
    }
}
