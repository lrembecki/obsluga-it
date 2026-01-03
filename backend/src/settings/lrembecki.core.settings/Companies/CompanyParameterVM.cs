namespace lrembecki.core.settings.Companies;

public record CompanyParameterVM(
    int Order,
    string Name,
    string Value)
{
    internal static CompanyParameterVM Map(CompanyParameterEntity parameter)
    {
        if (parameter == null) return null!;
        return new(
            Order: parameter.Order,
            Name: parameter.Name,
            Value: parameter.Value
        );
    }
}
