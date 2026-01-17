using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsPersonDto : IHasStorage<AboutUsPersonDto>
{
    public int Order { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;

    // IHasStorage implementation
    public StorageDto GetStorage() 
        => Image;

    public Guid? GetStorageId()
        => ImageId;

    public AboutUsPersonDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = storageId };
}
