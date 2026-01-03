using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.settings.Address;

internal class AddressEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Address1 { get; private set; } = string.Empty;
    public string Address2 { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public static AddressEntity Create(Guid id, AddressDto model)
    {
        var addressEntity = new AddressEntity
        {
            Id = id,
        };

        addressEntity.Update(model);

        return addressEntity;
    }

    public void Update(AddressDto model)
    {
        Address1 = model.Address1;
        Address2 = model.Address2;
        City = model.City;
        PostalCode = model.PostalCode;
        Country = model.Country;
    }
}
