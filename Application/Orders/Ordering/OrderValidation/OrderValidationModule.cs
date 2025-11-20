using IGCSELearningHub.Application.Orders.Ordering.OrderValidation.Interfaces;
using IGCSELearningHub.Application.Orders.Ordering.OrderValidation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering.OrderValidation;

public static class OrderValidationModule
{
    public static IServiceCollection AddOrderValidationModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderValidationService, OrderValidationService>();
        return services;
    }
}
