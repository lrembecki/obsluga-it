using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class HighlightEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;

    public static HighlightEntity Create(Guid id, HighlightDto model)
    {
        var entity = new HighlightEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(HighlightDto model)
    {
        Title = model.Title;
        Icon = model.Icon;
    }
}
