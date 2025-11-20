using IGCSELearningHub.Application.Payments.PaymentOrchestration.Interfaces;
using IGCSELearningHub.Infrastructure.Payments.Options;
using IGCSELearningHub.Infrastructure.Payments.Providers.VnPay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Infrastructure.Payments;

public static class PaymentsInfrastructureModule
{
    public static IServiceCollection AddPaymentsInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.Configure<VnPayOptions>(config.GetSection("Vnpay"));

        services.AddScoped<IPaymentGateway, VnPayPaymentGateway>();
        services.AddScoped<IPaymentCallbackParser, VnPayPaymentGateway>();
        return services;
    }
}
