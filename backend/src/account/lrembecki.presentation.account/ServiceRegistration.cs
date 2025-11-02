using lrembecki.core.account.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.presentation.account;

public static class ServiceRegistration
{
    public static void AddAccount(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IPermissionService, PermissionService>();
    }

    public static void MapAccount(this WebApplication app)
    {

        var group = app.MapGroup("/api/account")
            .WithTags("Account")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapAccount();
    }
}
