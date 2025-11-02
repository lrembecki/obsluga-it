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

internal class StorageService(IUnitOfWork uow) : BaseCrudService<StorageEntity, StorageVM, StorageDto>(uow), IStorageService
{
    private readonly IRepository<ImageStorageEntity> _images = uow.GetRepository<ImageStorageEntity>();
    private readonly IRepository<FileStorageEntity> _files = uow.GetRepository<FileStorageEntity>();

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
}
