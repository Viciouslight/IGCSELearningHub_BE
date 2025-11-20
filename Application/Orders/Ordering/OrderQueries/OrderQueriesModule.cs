using IGCSELearningHub.Application.Orders.Ordering.OrderQueries.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderQueries.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderQueries;

public static class OrderQueriesModule
{
    public static IServiceCollection AddOrderQueriesModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderQueryService, OrderQueryService>();
        return services;
    }
}
