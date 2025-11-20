using IGCSELearningHub.Application.Orders.OrderPlacement.Interfaces;
using IGCSELearningHub.Application.Orders.OrderPlacement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.OrderPlacement;

public static class OrderPlacementModule
{
    public static IServiceCollection AddOrderPlacementModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}
