using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class LoyalityProgramEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/loyality-programs")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Loyality Programs")
        .MapGet("/", (ISender sender) => sender.SendAsync(new LoyalityProgramsGetQuery()));
    }
}
