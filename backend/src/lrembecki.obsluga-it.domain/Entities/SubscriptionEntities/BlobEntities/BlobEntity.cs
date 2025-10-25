using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.BlobEntities;

public class BlobEntity : SubscriptionEntity, IHasId<Guid>
{
    public Guid Id { get; protected set; }
    public string Filename { get; protected set; } = default!;
    public string BlobUrl { get; protected set; } = default!;
    public string BlobPath { get; protected set; } = default!;
    public long? Size { get; protected set; } = default!;

    
    public static T Create<T>(BlobEntity blob) where T : BlobEntity, new()
        => Create<T>(blob.Id, blob.Filename, blob.BlobUrl, blob.BlobPath, blob.Size);

    public static T Create<T>(Guid id, string filename, string blobUrl, string blobPath, long? size) where T : BlobEntity, new()
    {
        var blob = new T
        {
            Id = id,
            Filename = filename,
            BlobUrl = blobUrl,
            BlobPath = blobPath,
            Size = size
        };

        return blob;
    }

    public void Update(BlobEntity blob) => Update(blob.Filename, blob.BlobUrl, blob.BlobPath, blob.Size);

    public void Update(string filename, string blobUrl, string blobPath, long? size)
    {
        Filename = filename;
        BlobUrl = blobUrl;
        BlobPath = blobPath;
        Size = size;
    }
}
