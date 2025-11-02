using lrembecki.core.account.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.account;

public static class AccountEndpoint
{
    internal static IEndpointRouteBuilder MapAccount(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] IAccountService accountService,
            CancellationToken ct
        ) => accountService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
