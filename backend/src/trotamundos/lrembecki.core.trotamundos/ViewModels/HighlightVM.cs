using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record HighlightVM(
    Guid Id,
    string Title,
    string Icon
)
{
    public static HighlightVM Map(HighlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new HighlightVM(
            entity.Id,
            entity.Title,
            entity.Icon
        );
    }
}

