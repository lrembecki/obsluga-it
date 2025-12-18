using lrembecki.core.storage.ViewModels;
using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record LoyalityProgramVM(
    Guid Id,
    Guid ImageId,
    StorageVM Image,
    string Title,
    string Name,
    string Description)
{
    public static LoyalityProgramVM Map(LoyalityProgramEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new LoyalityProgramVM(
            entity.Id,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            entity.Title,
            entity.Name,
            entity.Description
        );
    }
}

