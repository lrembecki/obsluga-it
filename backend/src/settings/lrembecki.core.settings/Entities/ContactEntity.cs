using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Dtos;

namespace lrembecki.core.settings.Entities;

public class ContactEntity : SubscriptionBaseEntity, IHasId<Guid>
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
