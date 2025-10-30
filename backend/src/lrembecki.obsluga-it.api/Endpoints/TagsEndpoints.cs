using lrembecki.obsluga_it.api;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TagsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tags")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Tags");

        group.MapGet("/", async (ITagService service) => await service.GetAllAsync());
        group.MapGet("/{id:guid}", async (Guid id, ITagService service) => { try { return Results.Ok(await service.GetByIdAsync(id)); } catch (ArgumentNullException) { return Results.NotFound(); } });
        group.MapPost("/", async (TagDto dto, ITagService service, CancellationToken ct) => { var vm = await service.CreateAsync(dto, ct); return Results.Created($"/api/tags/{vm.Id}", vm); });
        group.MapPut("/{id:guid}", async (Guid id, TagDto dto, ITagService service, CancellationToken ct) => { try { return Results.Ok(await service.UpdateAsync(id, dto, ct)); } catch (ArgumentNullException) { return Results.NotFound(); } });
        group.MapDelete("/{id:guid}", async (Guid id, ITagService service, CancellationToken ct) => { try { await service.DeleteAsync(id, ct); return Results.NoContent(); } catch (ArgumentNullException) { return Results.NotFound(); } });
    }
}
