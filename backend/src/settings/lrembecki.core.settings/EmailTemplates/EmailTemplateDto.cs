using lrembecki.core.storage;

namespace lrembecki.core.settings.EmailTemplates;

public record EmailTemplateDto
{
    public string Name { get; init; } = string.Empty;
    public Guid TemplateHtmlId { get; init; }
    public StorageDto TemplateHtml { get; init; } = null!;
    public string Subject { get; init; } = string.Empty;
    public List<string> Fields { get; init; } = [];
    public List<Guid> Contacts_to { get; init; } = [];
    public List<Guid> Contacts_cc { get; init; } = [];
    public List<Guid> Contacts_bcc { get; init; } = [];
}
