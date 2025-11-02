using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class AdvantageEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public static AdvantageEntity Create(Guid id, AdvantageDto model)
        => new AdvantageEntity
        {
            Id = id,
            Title = model.Title,
            Content = model.Content
        };

    public void Update(AdvantageDto model)
    {
        Title = model.Title;
        Content = model.Content;
    }
}
