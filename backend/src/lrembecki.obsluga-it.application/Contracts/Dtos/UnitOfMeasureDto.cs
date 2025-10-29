using lrembecki.obsluga_it.domain.Enums;

namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record UnitOfMeasureDto
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public UnitOfMeasureType Type { get; set; }
}