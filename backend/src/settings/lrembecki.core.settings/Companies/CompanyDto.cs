using lrembecki.core.settings.Address;

namespace lrembecki.core.settings.Companies;

public record CompanyDto
{
    public string Name { get; init; } = string.Empty;
    public Guid AddressId { get; init; }
    public AddressDto Address { get; init; } = null!;
    public List<CompanyParameterDto> Parameters { get; init; } = [];
    public List<Guid> PhoneContact { get; init; } = [];
    public List<Guid> EmailContact { get; init; } = [];
}
