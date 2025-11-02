using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class LoyalityProgramEntity : TrotamundosBaseEntity
{
    public Guid ImageId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static LoyalityProgramEntity Create(Guid id, LoyalityProgramDto model)
    {
        var entity = new LoyalityProgramEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(LoyalityProgramDto model)
    {
        Name = model.Name;
        Title = model.Title;
        Description = model.Description;
        ImageId = model.ImageId!.Value;
    }
}
