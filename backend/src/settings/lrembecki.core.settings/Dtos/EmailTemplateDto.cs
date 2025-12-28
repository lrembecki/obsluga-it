using lrembecki.core.storage.Dtos;

namespace lrembecki.core.settings.Dtos;

public record EmailTemplateDto
{
    public string Name { get; init; } = string.Empty;
    public Guid TemplateHtmlId { get; init; }
    public StorageDto TemplateHtml { get; init; } = null!;
    public List<string> Fields { get; init; } = [];
}
