using lrembecki.core.trotamundos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.trotamundos;

public static class TripsEndpoint
{
    internal static IEndpointRouteBuilder MapTrips(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] ITripService tripService,
            CancellationToken ct
        ) => tripService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
