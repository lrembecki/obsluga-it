using lrembecki.core.storage.Dtos;

namespace lrembecki.core.trotamundos.Dtos;

public record TripImageDto
{
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = default!;
    public int Order { get; init; }
}
