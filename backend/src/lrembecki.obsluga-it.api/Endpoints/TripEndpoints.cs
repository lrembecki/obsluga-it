using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TripEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/trips")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Trips")
        .MapGet("/", (ISender sender) => sender.SendAsync(new TripsGetQuery()));
    }
}
