using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

internal class Tag : SubscriptionEntity, IHasId<Guid>
{
	public Guid Id { get; private set; } = default!;
	public string Name { get; private set; } = default!;

	public static Tag Create(Guid tagId, string name)
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
