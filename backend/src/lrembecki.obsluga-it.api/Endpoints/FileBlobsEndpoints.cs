using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.infrastructure.Extensions;
using lrembecki.obsluga_it.infrastructure;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class FileBlobsEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files")
            .RequireAuthorization(AuthenticationExtensions.AzureAdUserScopePolicy)
            .WithTags("Files");

        group.MapGet("/", async (IFileBlobService service) =>
            ServiceCallResult.CreateSuccessResult(await service.GetAllAsync()));

        group.MapGet("/{id:guid}", async (Guid id, IFileBlobService service) =>
            ServiceCallResult.CreateSuccessResult(await service.GetByIdAsync(id)));

        group.MapPost("/", async (FileBlobDto dto, IFileBlobService service, CancellationToken ct) =>
            ServiceCallResult.CreateSuccessResult(await service.CreateAsync(dto, ct)));

        group.MapPut("/{id:guid}", async (Guid id, FileBlobDto dto, IFileBlobService service, CancellationToken ct) =>
            ServiceCallResult.CreateSuccessResult(await service.UpdateAsync(id, dto, ct)));

        group.MapDelete("/{id:guid}", async (Guid id, IFileBlobService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return ServiceCallResult.CreateSuccessResult(errorMessage: null);
        });
    }
}
