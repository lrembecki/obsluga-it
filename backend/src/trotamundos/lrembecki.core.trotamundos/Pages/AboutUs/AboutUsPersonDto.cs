using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsPersonDto
{
    public int Order { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid? ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
}
