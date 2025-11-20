using IGCSELearningHub.Application.Orders.Invoices;
using IGCSELearningHub.Application.Orders.Ordering;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddOrderingModule();
        services.AddBillingModule();
        return services;
    }
}
