using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsDto : IHasStorage<AboutUsDto>, IHasStorageCollection<AboutUsDto, AboutUsPersonDto>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
    public List<AboutUsItemDto> Items { get; init; } = [];
    public List<AboutUsPersonDto> Persons { get; init; } = [];

    public StorageDto GetStorage()
        => Image;

    public List<AboutUsPersonDto> GetStoragecollection()
        => new([.. Persons]);

    public Guid? GetStorageId()
        => ImageId;

    public AboutUsDto UpdateStorageCollection(List<AboutUsPersonDto> items)
        => this with { Persons = items };

    public AboutUsDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = storageId ?? Guid.Empty };
}
