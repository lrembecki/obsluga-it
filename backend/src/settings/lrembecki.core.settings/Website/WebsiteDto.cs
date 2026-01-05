namespace lrembecki.core.settings.Website;

public record WebsiteDto
{
    public string Title { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public Guid CompanyId { get; init; }
    public WebsiteMetaDto Meta { get; init; } = null!;
}
