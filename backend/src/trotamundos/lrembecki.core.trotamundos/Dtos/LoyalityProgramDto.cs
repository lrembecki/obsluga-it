using lrembecki.core.storage.Dtos;

namespace lrembecki.core.trotamundos.Dtos;

public record LoyalityProgramDto
{
    public Guid? ImageId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;


    public StorageDto Image { get; init; } = default!;
}