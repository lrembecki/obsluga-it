using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Entities;

namespace lrembecki.core.forms.FormDefinitions;


internal class FormDefinitionEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Guid? NotificationId { get; private set; }
    public NotificationEntity? Notification { get; private set; } = null!;

    public List<FormFieldDefinitionEntity> Fields { get; private set; } = [];
    public List<FormDefinitionEmailNotificationFieldMappingEntity> EmailNotificationFieldMapping { get; private set; } = [];

    public static FormDefinitionEntity Create(Guid id, FormDefinitionDto model)
    {
        var entity = new FormDefinitionEntity
        {
            Id = id,
            Name = model.Name
        };

        entity.AddDomainEvent(FormDefinitionsDomainEvent.Create(entity.Id));

        return entity;
    }

    public void Update(FormDefinitionDto model)
    {
        Name = model.Name;
        NotificationId = model.NotificationId;

        Fields.Clear();
        model.Fields.ForEach(m => Fields.Add(FormFieldDefinitionEntity.Create(Id, m)));

        EmailNotificationFieldMapping.Clear();
        model.EmailNotificationFieldMapping.ForEach(m => EmailNotificationFieldMapping.Add(FormDefinitionEmailNotificationFieldMappingEntity.Create(Id, m)));

        AddDomainEvent(FormDefinitionsDomainEvent.Create(Id));
    }
}
