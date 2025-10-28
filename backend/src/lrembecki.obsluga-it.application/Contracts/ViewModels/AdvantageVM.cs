using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record AdvantageVM(
    Guid Id,
    string Title,
    string Content
)
{
    internal static AdvantageVM MapFromDomainEntity(AdvantageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new AdvantageVM(
            entity.Id,
            entity.Title,
            entity.Content
        );
    }
}

