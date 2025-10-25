using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

public enum UnitOfMeasureType
{
	Currency
}

public class UnitOfMeasure : BaseEntity, IHasId<Guid>
{
	public Guid Id { get; set; } = Guid.Empty;
	public string Name { get; set; } = string.Empty;
	public string ShortName { get; set; } = string.Empty;
	public string ShortCode { get; set; } = string.Empty;
	public UnitOfMeasureType Type { get; set; }

	public static UnitOfMeasure Create (Guid unitOfMeasureId, UnitOfMeasureType type, string name, string shortName, string shortCode)
		=> new()
		{
			Id = unitOfMeasureId,
			Type = type,
			Name = name,
			ShortName = shortName,
            ShortCode = shortCode
        };
}
