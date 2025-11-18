using IGCSELearningHub.Application.Identity.Authentication.Interfaces;
using IGCSELearningHub.Application.Identity.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Identity.Authentication;

public static class AuthenticationModule
{
    public static IServiceCollection AddAuthenticationModule(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccessTokenFactory, AccessTokenFactory>();
        services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
