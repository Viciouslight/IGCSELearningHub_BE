using IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentOrchestration.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments.PaymentOrchestration;

public static class PaymentOrchestrationModule
{
    public static IServiceCollection AddPaymentOrchestrationModule(this IServiceCollection services)
    {
        services.AddScoped<IPaymentOrchestrator, PaymentOrchestrator>();
        return services;
    }
}
