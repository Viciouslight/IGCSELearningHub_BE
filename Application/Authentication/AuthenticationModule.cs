using Application.Authentication.Interfaces;
using Application.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Authentication;

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
