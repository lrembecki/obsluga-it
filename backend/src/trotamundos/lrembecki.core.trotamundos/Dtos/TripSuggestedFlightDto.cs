using lrembecki.core.storage.Dtos;

namespace lrembecki.core.trotamundos.Dtos;

public record TripSuggestedFlightDto
{
    public int Order { get; set; }
    public Guid? ImageId { get; set; } = default!;
    public StorageDto Image { get; init; } = default!;
}
