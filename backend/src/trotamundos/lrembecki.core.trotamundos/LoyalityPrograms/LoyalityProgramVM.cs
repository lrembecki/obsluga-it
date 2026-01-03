using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.LoyalityPrograms;

public record LoyalityProgramVM(
    Guid Id,
    Guid ImageId,
    StorageVM Image,
    string Title,
    string Name,
    string Description)
{
    internal static LoyalityProgramVM Map(LoyalityProgramEntity entity)
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
