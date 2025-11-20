using IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle;

public static class OrderLifecycleModule
{
    public static IServiceCollection AddOrderLifecycleModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderLifecycleService, OrderLifecycleService>();
        return services;
    }
}
