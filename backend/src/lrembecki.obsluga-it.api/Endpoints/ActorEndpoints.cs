using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class ActorEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/actors")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Actors")
        .MapGet("/", (ISender sender) => sender.SendAsync(new ActorsGetQuery()));
    }
}
