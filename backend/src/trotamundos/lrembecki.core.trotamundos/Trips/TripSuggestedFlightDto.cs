using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Trips;

public record TripSuggestedFlightDto
{
    public int Order { get; set; }
    public Guid? ImageId { get; set; } = default!;
    public StorageDto Image { get; init; } = default!;
}
