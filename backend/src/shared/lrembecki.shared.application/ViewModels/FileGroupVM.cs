using lrembecki.shared.domain.Entities;

namespace lrembecki.shared.application.ViewModels;

public record FileGroupVM(
    Guid Id,
    string Name
)
{
    public static FileGroupVM MapFromDomainEntity(FileGroupEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new FileGroupVM(
            entity.Id,
            entity.Name
        );
    }
}

