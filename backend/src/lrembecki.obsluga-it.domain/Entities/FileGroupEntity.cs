using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

public class FileGroupEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    public static FileGroupEntity Create(Guid fileGroupId, string name)
        => new ()
        {
            Id = fileGroupId,
            Name = name
        };
}
