using lrembecki.core.account.Dtos;
using lrembecki.core.account.Services;
using lrembecki.core.account.ViewModels;
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
        builder.Services.AddScoped<IPermissionGroupService, PermissionGroupService>();
        builder.Services.AddScoped<IAccountSubscriptionService, AccountSubscriptionService>();
    }

    public static void MapAccount(this WebApplication app)
    {

        var group = app.MapGroup("/api/account")
            .WithTags("Account")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IPermissionService, PermissionDto, PermissionVM>("permissions", r =>
        {
            r.RequireAuthorization(p => p.RequireRole("Security.Permissions"));
        });
        group.MapCrud<IPermissionGroupService, PermissionGroupDto, PermissionGroupVM>("permission-groups", r =>
        {
            r.RequireAuthorization(p => p.RequireRole("Security.PermissionGroups"));
        });
        group.MapCrud<IAccountSubscriptionService, AccountSubscriptionDto, AccountSubscriptionVM>("subscription-accounts", r =>
        {
            r.RequireAuthorization(p => p.RequireRole("Security.AccountSubscriptions"));
        });

        group.MapAccount();
    }
}
