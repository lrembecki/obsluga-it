using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.Home;

public record HomeOpinionDto : IHasStorage<HomeOpinionDto>
{
    public int Order { get; init; }
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;

    public StorageDto GetStorage()
        => Image;

    public Guid? GetStorageId()
        => ImageId;

    public HomeOpinionDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = storageId };
}
