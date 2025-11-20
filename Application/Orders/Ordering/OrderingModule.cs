using IGCSELearningHub.Application.Orders.Ordering.OrderLifecycle;
using IGCSELearningHub.Application.Orders.Ordering.OrderPlacement;
using IGCSELearningHub.Application.Orders.Ordering.OrderPricing;
using IGCSELearningHub.Application.Orders.Ordering.OrderQueries;
using IGCSELearningHub.Application.Orders.Ordering.OrderValidation;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Orders.Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services)
        {
            services.AddOrderPlacementModule();
            services.AddOrderQueriesModule();
            services.AddOrderLifecycleModule();
            services.AddOrderPricingModule();
            services.AddOrderValidationModule();
            return services;
        }
    }
}
