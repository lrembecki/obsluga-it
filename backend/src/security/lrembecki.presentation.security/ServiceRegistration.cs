using lrembecki.core.security.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace lrembecki.presentation.security;

public static class ServiceRegistration
{
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    }

    public static void MapSecurity(this WebApplication app)
    {
        var group = app.MapGroup("/api/authenticate")
            .WithTags("Authenticate")
            .RequireAuthorization("AzureAdUserScope");

        group.MapGet("/{subscriptionId:guid?}", (
            [FromServices] IAuthenticationService authService,
            [FromRoute] Guid? subscriptionId,
            ClaimsPrincipal principal,
            CancellationToken ct
        ) => authService.AuthenticateAsync(
            email: principal.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Upn)?.Value!, 
            subscriptionId: subscriptionId, 
            ct: ct)
        );
    }
}
