using lrembecki.obsluga_it.domain.Enums;
using lrembecki.shared.domain.Abstractions;
using lrembecki.shared.domain.Entities;

namespace lrembecki.obsluga_it.domain.Entities;

internal class UnitOfMeasureEntity : BaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; } = Guid.Empty;
    public string Name { get; private set; } = string.Empty;
    public string ShortName { get; private set; } = string.Empty;
    public string ShortCode { get; private set; } = string.Empty;
    public UnitOfMeasureType Type { get; private set; }

    public static UnitOfMeasureEntity Create(Guid unitOfMeasureId, UnitOfMeasureType type, string name, string shortName, string shortCode)
        => new()
        {
            Id = unitOfMeasureId,
            Type = type,
            Name = name,
            ShortName = shortName,
            ShortCode = shortCode
        };

    public void Update(UnitOfMeasureType type, string name, string shortName, string shortCode)
    {
        Type = type;
        Name = name;
        ShortName = shortName;
        ShortCode = shortCode;
    }
}
