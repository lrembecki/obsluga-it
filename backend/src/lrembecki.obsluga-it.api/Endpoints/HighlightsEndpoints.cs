using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class HighlightsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/highlights")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Highlights");

        group.MapGet("/", async (IHighlightService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (Guid id, IHighlightService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (HighlightDto dto, IHighlightService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (Guid id, HighlightDto dto, IHighlightService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (Guid id, IHighlightService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
