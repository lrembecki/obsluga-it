using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Files;

public record FileDto
{
    public Guid StorageId { get; init; }
    public StorageDto Storage { get; init; } = default!;
    public string Group { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Position { get; init; } = 0;
}
