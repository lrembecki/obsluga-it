using lrembecki.core.settings.Entities;
using lrembecki.core.storage.ViewModels;

namespace lrembecki.core.settings.ViewModels;

public record EmailTemplateVM(
    Guid Id,
    string Name,
    Guid TemplateHtmlId,
    StorageVM TemplateHtml,
    List<string> Fields
)
{ 
    internal static EmailTemplateVM Map(EmailTemplateEntity entity)
    {
        if (entity == null) return null!;

        return new(
            Id: entity.Id,
            Name: entity.Name,
            TemplateHtmlId: entity.TemplateHtmlId,
            TemplateHtml: StorageVM.Map(entity.TemplateHtml),
            Fields: [.. entity.Fields.Select(f => f.FieldName)]
        );
    }
}
