using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripRequirementVM(
    Guid TripId,
    int Order,
    string Description
)
{
    public static TripRequirementVM Map(TripRequirementEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripRequirementVM(
            entity.TripId,
            entity.Order,
            entity.Description
        );
    }
}

