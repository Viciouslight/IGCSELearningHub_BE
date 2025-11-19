using IGCSELearningHub.Application.Payments.PaymentMethods.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentMethods.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments.PaymentMethods;

public static class PaymentMethodsModule
{
    public static IServiceCollection AddPaymentMethodsModule(this IServiceCollection services)
    {
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        return services;
    }
}
