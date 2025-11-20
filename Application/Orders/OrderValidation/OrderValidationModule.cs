using IGCSELearningHub.Application.Orders.OrderValidation.Interfaces;
using IGCSELearningHub.Application.Orders.OrderValidation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.OrderValidation;

public static class OrderValidationModule
{
    public static IServiceCollection AddOrderValidationModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderValidationService, OrderValidationService>();
        return services;
    }
}
