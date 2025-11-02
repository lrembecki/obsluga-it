using lrembecki.core.trotamundos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.trotamundos;

public static class LoyalityProgramsEndpoint
{
    internal static IEndpointRouteBuilder MapLoyalityPrograms(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] ILoyalityProgramService loyalityProgramService,
            CancellationToken ct
        ) => loyalityProgramService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
