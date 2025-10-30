using lrembecki.obsluga_it.api;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class ActorsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/actors")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Actors");

        group.MapGet("/", async (IActorService service) => await service.GetAllAsync());

        group.MapGet("/{id:guid}", async (Guid id, IActorService service) =>
        {
            try { return Results.Ok(await service.GetByIdAsync(id)); } catch (ArgumentNullException) { return Results.NotFound(); }
        });

        group.MapPost("/", async (ActorDto dto, IActorService service, CancellationToken ct) =>
        {
            var vm = await service.CreateAsync(dto, ct);
            return Results.Created($"/api/actors/{vm.Id}", vm);
        });

        group.MapPut("/{id:guid}", async (Guid id, ActorDto dto, IActorService service, CancellationToken ct) =>
        {
            try { return Results.Ok(await service.UpdateAsync(id, dto, ct)); } catch (ArgumentNullException) { return Results.NotFound(); }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IActorService service, CancellationToken ct) =>
        {
            try { await service.DeleteAsync(id, ct); return Results.NoContent(); } catch (ArgumentNullException) { return Results.NotFound(); }
        });
    }
}
