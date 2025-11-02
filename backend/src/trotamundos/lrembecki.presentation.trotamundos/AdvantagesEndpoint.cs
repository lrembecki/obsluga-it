using lrembecki.core.storage.Services;
using lrembecki.core.trotamundos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.trotamundos;

public static class AdvantagesEndpoint
{
    internal static IEndpointRouteBuilder MapAdvantages(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] IAdvantageService advantageService,
            CancellationToken ct
        ) => advantageService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
