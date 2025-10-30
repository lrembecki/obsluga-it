using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using System.Security.Claims;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class AccountEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/account")
            .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
            .WithTags("Account");

        group.MapGet("/", async (IAuthenticationService auth, ClaimsPrincipal user) =>
        {
            var email = GetEmail(user);
            return ServiceCallResult.CreateSuccessResult(await auth.SignInAsync(email, null));
        });

        group.MapGet("/{subscriptionId:guid}", async (Guid subscriptionId, IAuthenticationService auth, ClaimsPrincipal user) =>
        {
            var email = GetEmail(user);
            return ServiceCallResult.CreateSuccessResult(await auth.SignInAsync(email, subscriptionId));
        });
    }

    private static string GetEmail(ClaimsPrincipal user) =>
        user.Claims.First(c => c.Type is "emails" or "preferred_username" or ClaimTypes.Email).Value;
}

