using lrembecki.core.trotamundos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.trotamundos;

public static class HighlightsEndpoint
{
    internal static IEndpointRouteBuilder MapHighlights(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] IHighlightService highlightService,
            CancellationToken ct
        ) => highlightService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
