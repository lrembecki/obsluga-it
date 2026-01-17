using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksItemDto : IHasStorage<HowItWorksItemDto>
{
    public int Order { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto? Image { get; init; }

    public StorageDto GetStorage()
        => Image!;

    public Guid? GetStorageId()
        => ImageId!;

    public HowItWorksItemDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = ImageId };
}
