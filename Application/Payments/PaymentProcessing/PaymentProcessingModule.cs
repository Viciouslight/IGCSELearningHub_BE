using IGCSELearningHub.Application.Payments.PaymentProcessing.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentProcessing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Payments.PaymentProcessing;

public static class PaymentProcessingModule
{
    public static IServiceCollection AddPaymentProcessingModule(this IServiceCollection services)
    {
        services.AddScoped<ICashPaymentService, CashPaymentService>();
        return services;
    }
}
