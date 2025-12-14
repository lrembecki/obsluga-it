using lrembecki.core;
using lrembecki.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation;

public static class CrudEndpointExtensions
{
    public static IEndpointRouteBuilder MapCrud<Tservice, Tdto, Tvm>(this IEndpointRouteBuilder endpoints, string endpoint, Action<RouteHandlerBuilder> configureEndpoint = null!)
        where Tservice : ICrudService<Tdto, Tvm>
    {
        var get = endpoints.MapGet($"/{endpoint}", async (
            [FromServices] Tservice service,
            CancellationToken ct
        ) => (await service.GetAllAsync(ct)).ToServiceCallResult())
            .WithTags("Get");

        configureEndpoint?.Invoke(get);

        var post = endpoints.MapPost($"/{endpoint}", async (
            [FromBody] Tdto model,
            [FromServices] Tservice service,
            CancellationToken ct
        ) => (await service.CreateAsync(model, ct)).ToServiceCallResult())
            .WithTags("Post");

        configureEndpoint?.Invoke(post);

        var put = endpoints.MapPut($"/{endpoint}/{{id:guid}}", async (
            [FromRoute] Guid id,
            [FromBody] Tdto model,
            [FromServices] Tservice service,
            CancellationToken ct
        ) => (await service.UpdateAsync(id, model, ct)).ToServiceCallResult())
            .WithTags("Put");

        configureEndpoint?.Invoke(put);

        var delete = endpoints.MapDelete($"/{endpoint}/{{id:guid}}", (
            [FromRoute] Guid id,
            [FromServices] Tservice service,
            CancellationToken ct
        ) => service.DeleteAsync(id, ct).ToServiceCallResult())
            .WithTags("Delete");

        configureEndpoint?.Invoke(delete);

        return endpoints;
    }
}
