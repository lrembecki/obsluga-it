using lrembecki.core.storage.Entities;

namespace lrembecki.core.storage.ViewModels;

public record StorageVM(
    Guid Id,
    string? Filename,
    string? BlobUrl,
    string? BlobPath,
    long? Size,
    FileStorageVM File,
    ImageStorageVM Image)
{
    public static StorageVM Map(StorageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new StorageVM(
            entity.Id,
            entity.Filename,
            entity.BlobUrl,
            entity.BlobPath,
            entity.Size,
            FileStorageVM.Map(entity.File),
            ImageStorageVM.Map(entity.Image)
        );
    }
}

