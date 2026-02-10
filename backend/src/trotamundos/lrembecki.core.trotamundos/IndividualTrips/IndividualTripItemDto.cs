
using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripItemDto : IHasStorage<IndividualTripItemDto>
{
    public int Order { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Uom { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;

    public StorageDto GetStorage() => Image;
    public Guid? GetStorageId() => ImageId;
    public IndividualTripItemDto UpdateStorageId(Guid? storageId)
        => this with { ImageId = storageId ?? Guid.Empty };
}
