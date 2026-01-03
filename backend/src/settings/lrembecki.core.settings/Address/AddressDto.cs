namespace lrembecki.core.settings.Address;

public record AddressDto
{
    public string Address1 { get; init; } = string.Empty;
    public string Address2 { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
}
