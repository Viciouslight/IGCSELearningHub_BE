using IGCSELearningHub.Application.Orders.OrderLifecycle.Interfaces;
using IGCSELearningHub.Application.Orders.OrderLifecycle.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.OrderLifecycle;

public static class OrderLifecycleModule
{
    public static IServiceCollection AddOrderLifecycleModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderLifecycleService, OrderLifecycleService>();
        return services;
    }
}
