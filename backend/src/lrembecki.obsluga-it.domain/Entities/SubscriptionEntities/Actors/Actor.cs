using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.Actors;

internal class Actor : SubscriptionEntity, IHasId<Guid>
{
	private readonly HashSet<Contact> _contacts = [];

	public Guid Id { get; private set; }
	public string? Firstname { get; private set; }
	public string? Lastname { get; private set; }

	public IReadOnlyCollection<Contact> Contacts => _contacts.ToList().AsReadOnly();

	public static Actor Create(Guid actorId, string? firstname, string? lastname)
	{
		var actor = new Actor
		{
			Id = actorId
		};
		actor.Update(firstname, lastname);
		return actor;
    }

	public void Update(string? firstname, string? lastname)
	{
		Firstname = firstname;
		Lastname = lastname;
    }

	public void AddContact(Contact contact) => _contacts.Add(contact);
    public void RemoveContact(Contact contact) => _contacts.Remove(contact);
}