using lrembecki.core.Markers;
using lrembecki.core.storage;
using lrembecki.core.shared.Subscriptions;
using lrembecki.core.settings.Contacts;

namespace lrembecki.core.settings.EmailTemplates;

internal class EmailTemplateEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid TemplateHtmlId { get; private set; }
    public StorageEntity TemplateHtml { get; private set; } = null!;
    public List<EmailTemplateFieldEntity> Fields { get; private set; } = [];

    public string Subject { get; private set; } = string.Empty;
    public List<ContactEntity> Contacts_to { get; private set; } = [];
    public List<ContactEntity> Contacts_bcc { get; private set; } = [];
    public List<ContactEntity> Contacts_cc { get; private set; } = [];

    public static EmailTemplateEntity Create(Guid id, EmailTemplateDto model)
    {
        var entity = new EmailTemplateEntity
        {
            Id = id,
        };

        entity.Update(model);
    
        return entity;
    }

    public void Update(EmailTemplateDto model)
    {
        Name = model.Name;
        TemplateHtmlId = model.TemplateHtmlId;
        Subject = model.Subject;

        Fields.Clear();
        Fields = model.Fields
            .Select(e => EmailTemplateFieldEntity.Create(Id, e))
            .ToList();
    }
}
