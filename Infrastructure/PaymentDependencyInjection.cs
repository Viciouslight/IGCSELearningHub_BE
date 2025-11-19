using IGCSELearningHub.Application.Payments.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentProcessing.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentProcessing.Services;
using IGCSELearningHub.Infrastructure.Payments.Options;
using IGCSELearningHub.Infrastructure.Payments.Providers;
using IGCSELearningHub.Infrastructure.Payments.Providers.VnPay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Infrastructure
{
    public static class PaymentDependencyInjection
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services, IConfiguration config)
        {
            var section = config.GetSection("Vnpay");
            services.Configure<VnPayOptions>(section);
            services.AddHttpContextAccessor();

            services.AddScoped<IPaymentGateway, VnPayPaymentGateway>();
            services.AddScoped<IPaymentOrchestrator, PaymentOrchestrator>();
            services.AddScoped<ICashPaymentService, CashPaymentService>();

            return services;
        }
    }
}
