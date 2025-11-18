using IGCSELearningHub.Application.Identity.Authentication.Interfaces;
using IGCSELearningHub.Application.IRepository;
using IGCSELearningHub.Infrastructure.Identity.Accounts;
using IGCSELearningHub.Infrastructure.Identity.Authentication;
using IGCSELearningHub.Infrastructure.Identity.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Infrastructure.Identity;

public static class IdentityInfrastructureModule
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddSingleton<IExternalAuthProvider, FirebaseExternalAuthProvider>();
        return services;
    }
}
