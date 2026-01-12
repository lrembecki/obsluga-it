using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
    public List<AboutUsItemDto> Items { get; init; } = [];
}
