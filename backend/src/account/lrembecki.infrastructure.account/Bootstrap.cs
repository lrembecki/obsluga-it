using lrembecki.core.account.Accounts;
using lrembecki.core.account.AccountSubscriptions;
using lrembecki.core.account.PermissionGroups;
using lrembecki.core.account.Permissions;
using lrembecki.infrastructure.shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapAccount
{
    public static ModelBuilder ApplyConfigurationFromAccount(this ModelBuilder modelBuilder)
    {
        var accountAssembly = typeof(BootstrapAccount).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(accountAssembly);
        return modelBuilder;
    }

    public static IServiceCollection AddAccount(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountSubscriptionService, AccountSubscriptionService>();
        services.AddScoped<IPermissionGroupService, PermissionGroupService>();
        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }

    public static WebApplication MapAccount(this WebApplication app)
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

        group.MapGet("/", (
            [FromServices] IAccountService accountService,
            CancellationToken ct
        ) => accountService.GetAllAsync(ct))
            .WithTags("Get");

        return app;
    }
}
