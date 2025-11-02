using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.storage.Dtos;

namespace lrembecki.core.storage.Entities;

public class StorageEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Filename { get; private set; } = default!;
    public string BlobUrl { get; private set; } = default!;
    public string BlobPath { get; private set; } = default!;
    public long Size { get; private set; } = default!;

    public FileStorageEntity File { get; private set; } = default!;
    public ImageStorageEntity Image { get; private set; } = default!;

    public static StorageEntity Create(Guid id, Guid subscriptionId, StorageDto model)
    {
        var blob = new StorageEntity
        {
            Id = id,
            SubscriptionId = subscriptionId
        };

        blob.Update(model);

        return blob;
    }

    public void Update(StorageDto model)
    {
        Filename = model.Filename!;
        BlobUrl = model.BlobUrl!;
        BlobPath = model.BlobPath!;
        Size = model.Size!.Value;

        if (model.File != null)
        {
            File ??= FileStorageEntity.Create(Id, model.File);
            File.Update(model.File);
        }

        if (model.Image != null)
        {
            Image ??= ImageStorageEntity.Create(Id, model.Image);
            Image.Update(model.Image);
        }
    }
}
