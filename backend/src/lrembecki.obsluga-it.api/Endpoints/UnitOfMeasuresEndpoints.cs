using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class UnitOfMeasuresEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/units")
            .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
            .WithTags("Units");

        group.MapGet("/", async (
            [FromServices] IUnitOfMeasureService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] IUnitOfMeasureService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (
            [FromBody] UnitOfMeasureDto dto, 
            [FromServices] IUnitOfMeasureService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromBody] UnitOfMeasureDto dto, 
            [FromServices] IUnitOfMeasureService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] IUnitOfMeasureService service, 
            CancellationToken ct
        ) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
