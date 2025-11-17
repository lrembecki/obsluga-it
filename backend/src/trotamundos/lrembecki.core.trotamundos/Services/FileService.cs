using lrembecki.core.Services;
using lrembecki.core.storage.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface IFileService : ICrudService<FileDto, FileVM>;
internal sealed class FileService(
    IUnitOfWork uow,
    IStorageService storage
) : BaseCrudService<FileEntity, FileVM, FileDto>(uow), IFileService
{
    protected override async Task<FileEntity> CreateEntity(Guid id, FileDto model, CancellationToken cancellationToken)
    {
        model = model with
        {
            StorageId = (await storage.CreateAsync(model.Storage, cancellationToken)).Id
        };

        var result = await  base.CreateEntity(id, model, cancellationToken);

        return result;
    }

    protected override async Task UpdateEntity(FileEntity entity, FileDto model)
    {
        await storage.UpdateAsync(entity.StorageId, model.Storage);
        await base.UpdateEntity(entity, model);
    }
}
