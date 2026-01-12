using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsItemDto
{
    public int Order { get; init; }
    public string Icon { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
