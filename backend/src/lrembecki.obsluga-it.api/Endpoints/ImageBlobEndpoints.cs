using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class ImageBlobEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/image-blobs")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Image Blobs")
        .MapGet("/", (ISender sender) => sender.SendAsync(new ImageBlobsGetQuery()));
    }
}
