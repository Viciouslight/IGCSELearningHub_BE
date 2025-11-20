using IGCSELearningHub.Application.Payments.PaymentProcessing.Interfaces;
using IGCSELearningHub.Application.Payments.PaymentProcessing.Services;
using IGCSELearningHub.Infrastructure.Payments.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Infrastructure
{
    public static class PaymentDependencyInjection
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddPaymentOrchestrationModule(config);
            services.AddScoped<ICashPaymentService, CashPaymentService>();

            return services;
        }
    }
}
