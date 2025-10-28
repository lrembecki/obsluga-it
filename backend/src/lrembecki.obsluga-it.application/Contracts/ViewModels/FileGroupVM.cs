using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record FileGroupVM(
    Guid Id,
    string Name
)
{
    internal static FileGroupVM MapFromDomainEntity(FileGroupEntity entity)
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

