using lrembecki.core.storage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.storage;

public static class StorageEndpoint
{
    internal static IEndpointRouteBuilder MapStorage(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (
            [FromServices] IStorageService storageService,
            CancellationToken ct
        ) => storageService.GetAllAsync(ct))
            .WithTags("Get");

        return endpoints;
    }
}
