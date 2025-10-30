using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class LoyalityProgramsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loyality-programs")
            .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
            .WithTags("LoyalityPrograms");

        group.MapGet("/", async (
            [FromServices] ILoyalityProgramService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] ILoyalityProgramService service
        ) => ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (
            [FromBody] LoyalityProgramDto dto, 
            [FromServices] ILoyalityProgramService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromBody] LoyalityProgramDto dto, 
            [FromServices] ILoyalityProgramService service, 
            CancellationToken ct
        ) => ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (
            [FromRoute] Guid id, 
            [FromServices] ILoyalityProgramService service, 
            CancellationToken ct
        ) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
