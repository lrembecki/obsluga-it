using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Trips;

public record TripImageDto
{
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = default!;
    public int Order { get; init; }
}
