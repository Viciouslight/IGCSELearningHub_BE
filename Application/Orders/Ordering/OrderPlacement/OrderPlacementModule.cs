using IGCSELearningHub.Application.Orders.Ordering.OrderPlacement.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderPlacement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderPlacement;

public static class OrderPlacementModule
{
    public static IServiceCollection AddOrderPlacementModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}
