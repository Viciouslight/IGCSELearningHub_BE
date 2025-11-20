using IGCSELearningHub.Application.Payments.PaymentCallbacks.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentCallbacks.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments.PaymentCallbacks;

public static class PaymentCallbacksModule
{
    public static IServiceCollection AddPaymentCallbacksModule(this IServiceCollection services)
    {
        services.AddScoped<IPaymentCallbackHandler, PaymentCallbackHandler>();
        return services;
    }
}
