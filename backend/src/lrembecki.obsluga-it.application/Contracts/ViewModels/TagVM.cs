using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TagVM(
    Guid Id,
    string Name
)
{
    internal static TagVM MapFromDomainEntity(TagEnity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TagVM(
            entity.Id,
            entity.Name
        );
    }
}

