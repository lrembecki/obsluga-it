using lrembecki.core.Markers;
using lrembecki.core.settings.Address;
using lrembecki.core.settings.Contacts;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.settings.Companies;

internal class CompanyEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid AddressId { get; private set; }
    public AddressEntity Address { get; private set; } = null!;
    public List<CompanyParameterEntity> Parameters { get; private set; } = [];
    public List<ContactEntity> PhoneContact { get; private set; } = [];
    public List<ContactEntity> EmailContact { get; private set; } = [];

    public static CompanyEntity Create(Guid id, CompanyDto model)
    {
        var companyEntity = new CompanyEntity
        {
            Id = id
        };

        companyEntity.Update(model);

        return companyEntity;
    }

    public void Update(CompanyDto model)
    {
        Name = model.Name;
        AddressId = model.AddressId;

        Parameters.Clear();
        Parameters = [.. model.Parameters.Select(e => CompanyParameterEntity.Create(Id, e)) ];
    }

}
