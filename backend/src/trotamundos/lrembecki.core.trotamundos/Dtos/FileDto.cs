using lrembecki.core.storage.Dtos;

namespace lrembecki.core.trotamundos.Dtos;

public record FileDto
{
    public Guid StorageId { get; init; }
    public StorageDto Storage { get; init; } = default!;
    public string Group { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Position { get; init; } = 0;
}
