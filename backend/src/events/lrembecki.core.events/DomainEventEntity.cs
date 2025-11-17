using lrembecki.core.Markers;

namespace lrembecki.core.events;

public class DomainEventEntity : IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime Created { get; private set; }
    public DateTime? Processed { get; private set; }

    public static DomainEventEntity Create(Guid id, string name)
    {
        return new DomainEventEntity
        {
            Id = id,
            Name = name,
            Created = DateTime.UtcNow
        };
    }
}
