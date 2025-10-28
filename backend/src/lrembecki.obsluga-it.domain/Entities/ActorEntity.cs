using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class ActorEntity : SubscriptionBaseEntity, IHasId<Guid>
{
	public Guid Id { get; private set; }
	public string? Firstname { get; private set; }
	public string? Lastname { get; private set; }

	public virtual List<ContactEntity> Contacts { get; private set; } = [];

	public static ActorEntity Create(Guid actorId, string? firstname, string? lastname)
	{
		var actor = new ActorEntity
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

	public void AddContact(ContactEntity contact) => Contacts.Add(contact);
    public void RemoveContact(ContactEntity contact) => Contacts.Remove(contact);
}