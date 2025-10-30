using lrembecki.shared.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

internal class LoyalityProgramEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public Guid ImageId { get; private set; }
    public ImageBlobEntity Image { get; private set; } = default!;
    public string Name { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static LoyalityProgramEntity Create(Guid id, string name, string title, string description, ImageBlobEntity image)
        => new LoyalityProgramEntity
        {
            Id = id,
            Name = name,
            Title = title,
            Description = description,
            Image = image,
            ImageId = image.Id
        };

    public void Update(string name, string title, string description, ImageBlobEntity image)
    {
        Name = name;
        Title = title;
        Description = description;
        Image = image;
    }
}
