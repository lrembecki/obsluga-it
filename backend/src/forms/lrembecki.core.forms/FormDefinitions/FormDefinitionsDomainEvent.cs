using lrembecki.core.Events;

namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionsDomainEvent(
    Guid RecordId,
    string EventType,
    DateTime UtcNow
 ) : PublishDomainEvent(
    Guid.NewGuid(),
    EventType,
    RecordId,
    UtcNow
)
{
    public static DomainEvent Create(Guid id)
        => new FormDefinitionsDomainEvent(
            id,
            typeof(FormDefinitionEntity).Name,
            DateTime.UtcNow);
}