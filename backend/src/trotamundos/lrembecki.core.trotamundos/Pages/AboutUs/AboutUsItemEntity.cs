namespace lrembecki.core.trotamundos.Pages.AboutUs;

internal class AboutUsItemEntity
{
    public Guid AboutUsId { get; private set; } 
    public int Order { get; private set; }
    public string Icon { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static AboutUsItemEntity Create(
        Guid aboutUsId,
        AboutUsItemDto model
    )
    {
        var entity = new AboutUsItemEntity
        {
            AboutUsId = aboutUsId,
        };

        entity.Update(model);
        
        return entity;
    }

    public  void Update(AboutUsItemDto model)
    {
        Order = model.Order;
        Icon = model.Icon;
        Title = model.Title;
        Description = model.Description;
    }
}