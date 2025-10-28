using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record LoyalityProgramVM(
    Guid Id, 
    string Title,
    string Name,
    string Description,
    ImageBlobVM Image)
{
    internal static LoyalityProgramVM MapFromDomainEntity(LoyalityProgramEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new LoyalityProgramVM(
            entity.Id, 
            entity.Title, 
            entity.Name, 
            entity.Description, 
            ImageBlobVM.MapFromDomainEntity(entity.Image)
        );
    }
}

