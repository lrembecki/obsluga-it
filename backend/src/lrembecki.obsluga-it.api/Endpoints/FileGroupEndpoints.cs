using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class FileGroupEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/file-groups")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("File Groups")
        .MapGet("/", (ISender sender) => sender.SendAsync(new FileGroupsGetQuery()));
    }
}
