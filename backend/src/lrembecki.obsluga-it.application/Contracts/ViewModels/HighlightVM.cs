using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record HighlightVM(
    Guid Id,
    string Title,
    string Icon
)
{
    internal static HighlightVM MapFromDomainEntity(HighlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new HighlightVM(
            entity.Id,
            entity.Title,
            entity.Icon
        );
    }
}

