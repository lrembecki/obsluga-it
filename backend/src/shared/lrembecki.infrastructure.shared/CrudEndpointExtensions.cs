using lrembecki.core;
using lrembecki.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Text.Json;

namespace lrembecki.infrastructure.shared;

public static class CrudEndpointExtensions
{
    public static IEndpointRouteBuilder MapCrud<Tservice, Tdto, Tvm, TFilter>(this IEndpointRouteBuilder endpoints, string endpoint, Action<RouteHandlerBuilder> configureEndpoint = null!)
        where Tservice : ICrudService<Tdto, Tvm>
    {
        var get = endpoints.MapGet($"/{endpoint}", async (
            [FromServices] IHttpContextAccessor httpContext,
            [FromServices] Tservice service,
            CancellationToken ct
        ) => (await service.GetAllAsync(ParseFilter<TFilter>(httpContext.HttpContext!), ct)).ToServiceCallResult())
            .WithTags("Get");

        configureEndpoint?.Invoke(get);
        ConfigureCommands<Tservice, Tdto, Tvm>(endpoints, endpoint, configureEndpoint);

        return endpoints;
    }

    public static IEndpointRouteBuilder MapCrud<Tservice, Tdto, Tvm>(this IEndpointRouteBuilder endpoints, string endpoint, Action<RouteHandlerBuilder> configureEndpoint = null!)
        where Tservice : ICrudService<Tdto, Tvm>
    {
        var get = endpoints.MapGet($"/{endpoint}", async (
            [FromServices] Tservice service,
            CancellationToken ct
        ) => (await service.GetAllAsync(ct)).ToServiceCallResult())
            .WithTags("Get");

        configureEndpoint?.Invoke(get);
        ConfigureCommands<Tservice, Tdto, Tvm>(endpoints, endpoint, configureEndpoint);

        return endpoints;
    }

    private static void ConfigureCommands<Tservice, Tdto, Tvm>(IEndpointRouteBuilder endpoints, string endpoint, Action<RouteHandlerBuilder>? configureEndpoint) where Tservice : ICrudService<Tdto, Tvm>
    {
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
    }
    private static TFilter ParseFilter<TFilter>(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.ContainsKey("filter"))
        {
            var filterRaw = httpContext.Request.Headers["filter"].ToString();
            var filter = JsonSerializer.Deserialize<TFilter>(filterRaw, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (filter != null)
            {
                return filter;
            }
        }

        return Activator.CreateInstance<TFilter>();
    }
}
