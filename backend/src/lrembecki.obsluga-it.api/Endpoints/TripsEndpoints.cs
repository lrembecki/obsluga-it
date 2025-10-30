using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TripsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trips")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Trips");



        group.MapGet("/", async (ITripService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));



        group.MapGet("/{id:guid}", async (Guid id, ITripService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));



        group.MapPost("/", async (TripDto dto, ITripService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));



        group.MapPut("/{id:guid}", async (Guid id, TripDto dto, ITripService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));



        group.MapDelete("/{id:guid}", async (Guid id, ITripService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
