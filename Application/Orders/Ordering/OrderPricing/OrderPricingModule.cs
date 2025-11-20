using IGCSELearningHub.Application.Orders.Ordering.OrderPricing.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderPricing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPricing;

public static class OrderPricingModule
{
    public static IServiceCollection AddOrderPricingModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderPricingService, OrderPricingService>();
        return services;
    }
}
