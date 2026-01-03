namespace lrembecki.core.settings.Companies;

public record CompanyParameterDto
{
    public int Order { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
}
