using lrembecki.core.storage.ViewModels;
using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record LoyalityProgramVM(
    Guid Id,
    Guid ImageId,
    string Title,
    string Name,
    string Description)
{
    public StorageVM Image { get; init; } = default!;

    public static LoyalityProgramVM Map(LoyalityProgramEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new LoyalityProgramVM(
            entity.Id,
            entity.ImageId,
            entity.Title,
            entity.Name,
            entity.Description
        );
    }
}

