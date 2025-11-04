using lrembecki.core;
using lrembecki.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation;

public static class CrudEndpointExtensions
{
    public static IEndpointRouteBuilder MapCrud<Tservice, Tdto, Tvm>(this IEndpointRouteBuilder endpoints, string endpoint)
        where Tservice : ICrudService<Tdto, Tvm>
    {
        endpoints.MapGet($"/{endpoint}", async (
            [FromServices] Tservice contactService,
            CancellationToken ct
        ) => (await contactService.GetAllAsync(ct)).ToServiceCallResult())
            .WithTags("Get");

        endpoints.MapPost($"/{endpoint}", async (
            [FromBody] Tdto model,
            [FromServices] Tservice contactService,
            CancellationToken ct
        ) => (await contactService.CreateAsync(model, ct)).ToServiceCallResult())
            .WithTags("Post");

        endpoints.MapPut($"/{endpoint}/{{id:guid}}", async (
            [FromRoute] Guid id,
            [FromBody] Tdto model,
            [FromServices] Tservice contactService,
            CancellationToken ct
        ) => (await contactService.UpdateAsync(id, model, ct)).ToServiceCallResult())
            .WithTags("Put");

        endpoints.MapDelete($"/{endpoint}/{{id:guid}}", (
            [FromRoute] Guid id,
            [FromServices] Tservice contactService,
            CancellationToken ct
        ) => contactService.DeleteAsync(id, ct).ToServiceCallResult())
            .WithTags("Delete");

        return endpoints;
    }
}
