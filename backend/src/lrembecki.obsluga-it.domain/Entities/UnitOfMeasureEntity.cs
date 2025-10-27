using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Enums;

namespace lrembecki.obsluga_it.domain.Entities;

internal class UnitOfMeasureEntity : BaseEntity, IHasId<Guid>
{
	public Guid Id { get; set; } = Guid.Empty;
	public string Name { get; set; } = string.Empty;
	public string ShortName { get; set; } = string.Empty;
	public string ShortCode { get; set; } = string.Empty;
	public UnitOfMeasureType Type { get; set; }

	public static UnitOfMeasureEntity Create (Guid unitOfMeasureId, UnitOfMeasureType type, string name, string shortName, string shortCode)
		=> new()
		{
			Id = unitOfMeasureId,
			Type = type,
			Name = name,
			ShortName = shortName,
            ShortCode = shortCode
        };
}
