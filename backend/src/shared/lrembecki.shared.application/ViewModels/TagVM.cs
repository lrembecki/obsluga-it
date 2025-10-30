using lrembecki.shared.domain.Entities;

namespace lrembecki.shared.application.ViewModels;

public record TagVM(
    Guid Id,
    string Name
)
{
    public static TagVM MapFromDomainEntity(TagEntity entity)
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

