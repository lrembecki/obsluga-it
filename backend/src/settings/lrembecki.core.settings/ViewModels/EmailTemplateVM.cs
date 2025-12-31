using lrembecki.core.settings.Entities;
using lrembecki.core.storage.ViewModels;

namespace lrembecki.core.settings.ViewModels;

public record EmailTemplateVM(
    Guid Id,
    string Name,
    Guid TemplateHtmlId,
    StorageVM TemplateHtml,
    string Subject,
    List<string> Fields,
    List<Guid> Contacts_to,
    List<Guid> Contacts_cc,
    List<Guid> Contacts_bcc
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
            Subject: entity.Subject,
            Fields: [.. entity.Fields.Select(f => f.FieldName)],
            Contacts_to: [.. entity.Contacts_to.Select(e => e.Id)],
            Contacts_cc: [.. entity.Contacts_cc.Select(e => e.Id)],
            Contacts_bcc: [.. entity.Contacts_bcc.Select(e => e.Id)]
        );
    }
}
