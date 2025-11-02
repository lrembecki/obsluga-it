using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record AdvantageVM(
    Guid Id,
    string Title,
    string Content
)
{
    public static AdvantageVM Map(AdvantageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new AdvantageVM(
            entity.Id,
            entity.Title,
            entity.Content
        );
    }
}

