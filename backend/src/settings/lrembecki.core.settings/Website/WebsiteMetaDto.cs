using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.settings.Website;

public record WebsiteMetaDto : IHasStorage<WebsiteMetaDto>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Keywords { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;

    public StorageDto GetStorage()
        => Image;

    public Guid? GetStorageId()
        => ImageId;

    public WebsiteMetaDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = storageId };
}
