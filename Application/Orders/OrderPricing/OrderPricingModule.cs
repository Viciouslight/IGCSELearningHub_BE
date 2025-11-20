using Microsoft.Extensions.DependencyInjection;
using IGCSELearningHub.Application.Orders.OrderPricing.Interfaces;
using IGCSELearningHub.Application.Orders.OrderPricing.Services;

namespace IGCSELearningHub.Application.Orders.OrderPricing;

public static class OrderPricingModule
{
    public static IServiceCollection AddOrderPricingModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderPricingService, OrderPricingService>();
        return services;
    }
}
