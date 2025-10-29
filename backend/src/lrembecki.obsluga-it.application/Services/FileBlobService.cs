using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;

internal sealed class FileBlobService(IUnitOfWork uow, IBlobService blobService) : IFileBlobService
{
    private readonly IRepository<FileBlobEntity> _files = uow.GetRepository<FileBlobEntity>();

    public async Task<FileBlobVM> CreateAsync(FileBlobDto model, CancellationToken cancellationToken = default)
    {
        model = await blobService.SyncBlobDataAsync(model, "files", cancellationToken);

        var fileBlob = BlobBaseEntity.Create<FileBlobEntity>(
            id: model.Id!.Value, 
            filename: model.Filename, 
            blobUrl: model.BlobUrl, 
            blobPath: model.BlobPath, 
            size: model.Size
        );

        fileBlob.Update(
            description: model.Description, 
            displayName: model.DisplayName, 
            position: model.Position, 
            group: model.FileGroupId is null
                ? null
                : await uow.GetRepository<FileGroupEntity>().FindByIdAsync(model.FileGroupId.Value, cancellationToken)
        );

        await _files.AddAsync(fileBlob);

        return await GetByIdAsync(fileBlob.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var fileBlob = await RequireByIdAsync(id, cancellationToken);
        await _files.DeleteAsync(fileBlob);
    }

    public Task<List<FileBlobVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => _files.SelectAsync(e => FileBlobVM.MapFromDomainEntity(e));

    public async Task<FileBlobVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => FileBlobVM.MapFromDomainEntity(await RequireByIdAsync(id, cancellationToken));

    public async Task<FileBlobVM> UpdateAsync(Guid id, FileBlobDto model, CancellationToken cancellationToken = default)
    {
        var fileBlob = await RequireByIdAsync(id, cancellationToken);

        if (model.BinaryData is not null) await blobService.RemoveBlobAsync(fileBlob.BlobPath!, "files", cancellationToken);

        model = await blobService.SyncBlobDataAsync(model, "files", cancellationToken);

        fileBlob.Update(model.Filename, model.BlobUrl, model.BlobPath, model.Size);

        await _files.UpdateAsync(fileBlob);

        return await GetByIdAsync(fileBlob.Id, cancellationToken);
    }

    private async Task<FileBlobEntity> RequireByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _files.FindByIdAsync(id, cancellationToken)
        ?? throw new ArgumentNullException(nameof(id), "File not found");
}