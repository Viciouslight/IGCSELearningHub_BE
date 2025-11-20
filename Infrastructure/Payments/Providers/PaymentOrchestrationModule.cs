using IGCSELearningHub.Application.Payments.Interfaces;
using IGCSELearningHub.Infrastructure.Payments.Options;
using IGCSELearningHub.Infrastructure.Payments.Providers.VnPay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Infrastructure.Payments.Providers;

public static class PaymentOrchestrationModule
{
    public static IServiceCollection AddPaymentOrchestrationModule(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("Vnpay");
        services.Configure<VnPayOptions>(section);
        services.AddHttpContextAccessor();

        services.AddScoped<IPaymentGateway, VnPayPaymentGateway>();
        services.AddScoped<IPaymentOrchestrator, PaymentOrchestrator>();
        return services;
    }
}
