using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class TagsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tags")
        .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
        .WithTags("Tags");

        group.MapGet("/", async (ITagService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (Guid id, ITagService service) =>
        ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (TagDto dto, ITagService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (Guid id, TagDto dto, ITagService service, CancellationToken ct) =>
        ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (Guid id, ITagService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
