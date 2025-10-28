using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class ContactEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; } = default!;
    public Guid ActorId { get; private set; }

    public string Email { get; private set; } = default!;
    public string Phone { get; private set; } = default!;

    public static ContactEntity Create(Guid contactId, string email, string phone)
    {
        ContactEntity contact = new()
        {
            Id = contactId
        };

        contact.Update(email, phone);

        return contact;
    }

    public void Update(string email, string phone)
    {
        Email = email;
        Phone = phone;
    }
}
