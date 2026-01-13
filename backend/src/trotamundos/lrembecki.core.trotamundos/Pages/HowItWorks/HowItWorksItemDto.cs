using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksItemDto
{
    public int Order { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto? Image { get; init; }
}
