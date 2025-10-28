using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TagEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/tags")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Tags")
        .MapGet("/", (ISender sender) => sender.SendAsync(new TagsGetQuery()));
    }
}
