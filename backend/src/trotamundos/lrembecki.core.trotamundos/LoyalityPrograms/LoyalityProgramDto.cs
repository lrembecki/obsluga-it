using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.LoyalityPrograms;

public record LoyalityProgramDto
{
    public Guid? ImageId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public StorageDto Image { get; init; } = default!;
}