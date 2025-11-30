using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.core.storage.Dtos;
using lrembecki.core.storage.Entities;
using lrembecki.core.storage.ViewModels;

namespace lrembecki.core.storage.Services;

public interface IStorageService : ICrudService<StorageDto, StorageVM>
{
    Task<List<StorageVM>> GetAllImagesAsync(List<Guid>? imageIds, CancellationToken cancellationToken);
    Task<List<StorageVM>> GetAllFilesAsync(CancellationToken cancellationToken);
}

internal class StorageService(IUnitOfWork uow, IBlobHelper blobHelper) : BaseCrudService<StorageEntity, StorageVM, StorageDto>(uow), IStorageService
{
    private readonly IRepository<ImageStorageEntity> _images = uow.GetRepository<ImageStorageEntity>();
    private readonly IRepository<FileStorageEntity> _files = uow.GetRepository<FileStorageEntity>();

    protected override async Task<StorageEntity> CreateEntity(Guid id, StorageDto model, CancellationToken cancellationToken)
        => await base.CreateEntity(id, await UploadBlob(id, model, null!), cancellationToken);

    protected override async Task UpdateEntity(StorageEntity entity, StorageDto model)
        => await base.UpdateEntity(entity, await UploadBlob(entity.Id, model, entity.BlobPath));

    public Task<List<StorageVM>> GetAllImagesAsync(List<Guid>? imageIds, CancellationToken cancellationToken)
        => Task.Run(() =>
            _repository.GetAll()
                .Join(_images.GetAll(), e => e.Id, e => e.StorageId, (a, b) => a)
                .Where(a => imageIds == null || imageIds.Contains(a.Id))
                .Select(StorageVM.Map)
                .ToList(), cancellationToken);

    public Task<List<StorageVM>> GetAllFilesAsync(CancellationToken cancellationToken)
        => Task.Run(() =>
            _repository.GetAll()
                .Join(_files.GetAll(), e => e.Id, e => e.StorageId, (a, b) => a)
                .Select(StorageVM.Map)
                .ToList(), cancellationToken);

    protected override async Task DeleteEntity(StorageEntity entity, CancellationToken cancellationToken)
    {
        await base.DeleteEntity(entity, cancellationToken);
        await blobHelper.RemoveBlobAsync(entity.BlobPath, "storage", cancellationToken);
    }

    private async Task<StorageDto> UploadBlob(Guid id, StorageDto model, string existingBlobPath)
    {
        if (!string.IsNullOrEmpty(model.BinaryData))
        {
            if (!string.IsNullOrEmpty(existingBlobPath))
            {
                await blobHelper.RemoveBlobAsync(existingBlobPath, "storage", default);
            }

            if (model.Image != null)
            {
                model = model with
                {
                    BinaryData = model.BinaryData!.Replace("data:image/png;base64,", "")
                        .Replace("data:image/jpg;base64,", "")
                        .Replace("data:image/jpeg;base64,", "")
                };
            }

            model = model with
            {
                BlobPath = $"{id}/{model.Filename!}"
            };

            var ms = new MemoryStream(Convert.FromBase64String(model.BinaryData ?? string.Empty));

            model = model with
            {
                BlobUrl = await blobHelper.UploadBlobAsync(
                    "storage",
                    model.BlobPath!,
                    model.Filename!,
                    ms,
                    new Dictionary<string, string> { },
                    default)
            };
        }

        return model;
    }
}
