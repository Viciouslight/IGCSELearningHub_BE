using IGCSELearningHub.Application.Identity.Accounts.Interfaces;
using IGCSELearningHub.Application.Identity.Accounts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGCSELearningHub.Application.Identity.Accounts;

public static class AccountsModule
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}
