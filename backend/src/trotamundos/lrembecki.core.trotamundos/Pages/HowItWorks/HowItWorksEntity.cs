using lrembecki.core.Events;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

internal class HowItWorksEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string HeaderText { get; private set; } = string.Empty;
    public string FooterText { get; private set; } = string.Empty;

    public List<HowItWorksItemEntity> Items { get; private set; } = [];

    public static HowItWorksEntity Create(
        Guid id,
        HowItWorksDto model
    )
    {
        var entity = new HowItWorksEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(HowItWorksDto model)
    {
        Title = model.Title;
        HeaderText = model.HeaderText;
        FooterText = model.FooterText;

        // Update Items
        Items.Clear();
        Items.AddRange(model.Items.Select(item => HowItWorksItemEntity.Create(Id, item)));

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}
