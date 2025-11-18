using IGCSELearningHub.Application.Identity.Accounts;
using IGCSELearningHub.Application.Identity.Authentication;
using IGCSELearningHub.Application.Identity.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {
        services.AddAccountsModule();
        services.AddAuthenticationModule();
        services.AddDevicesModule();
        return services;
    }
}
