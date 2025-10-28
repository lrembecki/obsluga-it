using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class HighlightEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/highlights")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Highlights")
        .MapGet("/", (ISender sender) => sender.SendAsync(new HighlightsGetQuery()));
    }
}
