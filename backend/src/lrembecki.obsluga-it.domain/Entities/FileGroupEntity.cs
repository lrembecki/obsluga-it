using lrembecki.shared.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

internal class FileGroupEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    public static FileGroupEntity Create(Guid fileGroupId, string name)
        => new()
        {
            Id = fileGroupId,
            Name = name
        };

    public void Update(string name)
    {
        Name = name;
    }
}
