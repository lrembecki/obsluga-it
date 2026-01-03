namespace lrembecki.core.settings.Companies;

internal class CompanyParameterEntity
{
    public Guid CompanyId { get; private set; }
    public int Order { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public static CompanyParameterEntity Create(Guid companyId, CompanyParameterDto model)
    {
        return new CompanyParameterEntity
        {
            CompanyId = companyId,
            Order = model.Order,
            Name = model.Name,
            Value = model.Value ?? string.Empty
        };
    }
}
