using IGCSELearningHub.Application.Payments.PaymentMethods;
using IGCSELearningHub.Application.Payments.PaymentProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments;

public static class PaymentModule
{
    public static IServiceCollection AddPaymentModule(this IServiceCollection services)
    {
        services.AddPaymentMethodsModule();
        services.AddPaymentProcessingModule();
        return services;
    }
}
