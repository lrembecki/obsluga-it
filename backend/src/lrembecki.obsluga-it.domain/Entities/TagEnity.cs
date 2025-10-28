using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class TagEnity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public static TagEnity Create(Guid tagId, string name)
        => new()
        {
            Id = tagId,
            Name = name
        };

    public void Update(string name)
    {
        Name = name;
    }
}
