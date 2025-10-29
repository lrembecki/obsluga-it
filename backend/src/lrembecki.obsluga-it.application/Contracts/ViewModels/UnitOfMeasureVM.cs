using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Enums;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record UnitOfMeasureVM(
 Guid Id,
 string Name,
 string ShortName,
 string ShortCode,
 UnitOfMeasureType Type
)
{
    internal static UnitOfMeasureVM MapFromDomainEntity(UnitOfMeasureEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new UnitOfMeasureVM(
        entity.Id,
        entity.Name,
        entity.ShortName,
        entity.ShortCode,
        entity.Type
        );
    }
}
