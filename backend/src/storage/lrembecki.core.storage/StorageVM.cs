namespace lrembecki.core.storage;

public record StorageVM(
    Guid Id,
    string? Filename,
    string? BlobUrl,
    string? BlobPath,
    long? Size,
    FileStorageVM File,
    ImageStorageVM Image)
{
    internal static StorageVM Map(StorageEntity entity)
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
