using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class SubscriptionEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/subscriptions")
           .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
           .WithTags("Subscriptions")
           .MapGet("/", (ISender sender) => sender.SendAsync(new SubscriptionsGetQuery()));
    }
}

