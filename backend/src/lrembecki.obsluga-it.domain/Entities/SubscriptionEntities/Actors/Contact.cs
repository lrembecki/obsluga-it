using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.Actors;

internal class Contact : SubscriptionEntity, IHasId<Guid>
{
	public Guid Id { get; private set; } = default!;
	public Guid ActorId { get; private set; }

	public Email Email { get; private set; } = default!;
	public Phone Phone { get; private set; } = default!;

	public static Contact Create(Guid contactId, string email, string phone)
	{
		Contact contact = new()
        {
            Id = contactId
        };

		contact.Update(email, phone);

		return contact;
    }

	public void Update(string email, string phone)
	{
		Email = new Email(email);
		Phone = new Phone(phone);
    }
}
