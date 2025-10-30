using lrembecki.shared.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

internal class TagEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    public static TagEntity Create(Guid tagId, string name)
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
