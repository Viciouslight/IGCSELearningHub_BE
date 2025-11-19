using IGCSELearningHub.Application.Payments.PaymentMethods;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments;

public static class PaymentModule
{
    public static IServiceCollection AddPaymentModule(this IServiceCollection services)
    {
        services.AddPaymentMethodsModule();
        return services;
    }
}
