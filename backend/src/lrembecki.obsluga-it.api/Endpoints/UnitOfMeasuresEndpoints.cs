using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class UnitOfMeasuresEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/units")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Units");

        group.MapGet("/", async (IUnitOfMeasureService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (Guid id, IUnitOfMeasureService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (UnitOfMeasureDto dto, IUnitOfMeasureService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (Guid id, UnitOfMeasureDto dto, IUnitOfMeasureService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (Guid id, IUnitOfMeasureService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
