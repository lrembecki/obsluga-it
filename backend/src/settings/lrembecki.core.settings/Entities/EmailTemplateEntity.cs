using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Dtos;
using lrembecki.core.storage.Entities;

namespace lrembecki.core.settings.Entities;

internal class EmailTemplateEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid TemplateHtmlId { get; private set; }
    public StorageEntity TemplateHtml { get; private set; } = null!;
    public List<EmailTemplateFieldEntity> Fields { get; private set; } = [];

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

        Fields.Clear();
        Fields = model.Fields
            .Select(e => EmailTemplateFieldEntity.Create(Id, e))
            .ToList();
    }
}
