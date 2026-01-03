using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Settings;

public record AboutUsDto
{
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
