using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

public class FileGroup : SubscriptionEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    public static FileGroup Create(Guid fileGroupId, string name)
        => new ()
        {
            Id = fileGroupId,
            Name = name
        };
}
