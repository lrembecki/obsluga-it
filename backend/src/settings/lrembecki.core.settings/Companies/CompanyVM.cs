using lrembecki.core.settings.Address;

namespace lrembecki.core.settings.Companies;

public record CompanyVM(
    Guid Id,
    string Name,
    Guid AddressId,
    AddressVM Address,
    List<CompanyParameterVM> Parameters,
    List<Guid> PhoneContact,
    List<Guid> EmailContact)
{
    internal static CompanyVM Map(CompanyEntity company)
    {
        if (company == null) return null!;
        return new(
            Id: company.Id,
            Name: company.Name,
            AddressId: company.AddressId,
            Address: AddressVM.Map(company.Address),
            Parameters: [.. company.Parameters.Select(CompanyParameterVM.Map)],
            PhoneContact: [.. company.PhoneContact.Select(e => e.Id)],
            EmailContact: [.. company.EmailContact.Select(e => e.Id)]
        );
    }
}
