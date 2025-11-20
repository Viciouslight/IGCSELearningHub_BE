using IGCSELearningHub.Application.Orders.OrderLifecycle;
using IGCSELearningHub.Application.Orders.OrderPlacement;
using IGCSELearningHub.Application.Orders.OrderPricing;
using IGCSELearningHub.Application.Orders.OrderQueries;
using IGCSELearningHub.Application.Orders.OrderValidation;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddOrderPlacementModule();
        services.AddOrderQueriesModule();
        services.AddOrderLifecycleModule();
        services.AddOrderPricingModule();
        services.AddOrderValidationModule();
        return services;
    }
}
