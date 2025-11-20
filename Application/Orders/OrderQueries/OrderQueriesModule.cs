using IGCSELearningHub.Application.Orders.OrderQueries.Interfaces;
using IGCSELearningHub.Application.Orders.OrderQueries.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.OrderQueries;

public static class OrderQueriesModule
{
    public static IServiceCollection AddOrderQueriesModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderQueryService, OrderQueryService>();
        return services;
    }
}
