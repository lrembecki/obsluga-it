using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.settings.Contacts;

internal class ContactEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public string Position { get; private set; } = string.Empty;
    public string? Email { get; private set; } = string.Empty;
    public string? Phone { get; private set; } = string.Empty;

    public static ContactEntity Create(Guid id, ContactDto model)
    {
        var entity = new ContactEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(ContactDto model)
    {
        Name = model.Name;
        IsActive = model.IsActive;
        Position = model.Position;
        Email = model.Email;
        Phone = model.Phone;
    }
}
