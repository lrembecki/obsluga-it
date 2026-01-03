namespace lrembecki.core.settings.Address;

public record AddressVM(
    Guid Id,
    string Address1,
    string Address2,
    string City,
    string PostalCode,
    string Country)
{
    internal static AddressVM Map(AddressEntity address)
    {
        if (address == null) return null!;
        return new(
            Id: address.Id,
            Address1: address.Address1,
            Address2: address.Address2,
            City:  address.City,
            PostalCode: address.PostalCode,
            Country: address.Country
        );
    }
}
