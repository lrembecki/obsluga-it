using lrembecki.core.storage;

namespace lrembecki.core.settings.Website;

public record WebsiteMetaDto
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Keywords { get; init; } = string.Empty;
    public Guid ImageId { get; init; }
    public StorageDto Image { get; init; } = null!;
    public Guid CompanyId { get; init; }
}
