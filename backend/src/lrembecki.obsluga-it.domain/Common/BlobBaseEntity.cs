using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Common;

internal class BlobBaseEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; protected set; }
    public string? Filename { get; protected set; } = default!;
    public string? BlobUrl { get; protected set; } = default!;
    public string? BlobPath { get; protected set; } = default!;
    public long? Size { get; protected set; } = default!;


    public static T Create<T>(BlobBaseEntity blob) where T : BlobBaseEntity, new()
        => Create<T>(blob.Id, blob.Filename, blob.BlobUrl, blob.BlobPath, blob.Size);

    public static T Create<T>(Guid id, string? filename, string? blobUrl, string? blobPath, long? size) where T : BlobBaseEntity, new()
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

    public void Update(BlobBaseEntity blob) => Update(blob.Filename, blob.BlobUrl, blob.BlobPath, blob.Size);

    public void Update(string? filename, string? blobUrl, string? blobPath, long? size)
    {
        Filename = filename;
        BlobUrl = blobUrl;
        BlobPath = blobPath;
        Size = size;
    }
}
