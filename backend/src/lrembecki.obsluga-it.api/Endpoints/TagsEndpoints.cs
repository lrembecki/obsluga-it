using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using lrembecki.shared.application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TagsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tags")
            .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
            .WithTags("Tags");

        group.MapGet("/", async (
            [FromServices] ITagService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] ITagService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (
            [FromBody] TagDto dto, 
            [FromServices] ITagService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromBody] TagDto dto, 
            [FromServices] ITagService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] ITagService service, 
            CancellationToken ct
        ) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
