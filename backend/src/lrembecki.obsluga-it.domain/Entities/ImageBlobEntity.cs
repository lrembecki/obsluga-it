using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class ImageBlobEntity : BlobSubscriptionBaseEntity
{
	private readonly HashSet<TagEnity> _tags = [];

	public string? DisplayName { get; private set; } = default!;
	public string? Description { get; private set; } = default!;
	public long? Width { get; private set; } = default!;
	public long? Height { get; private set; } = default!;

	public IReadOnlyCollection<TagEnity> Tags => _tags.ToList().AsReadOnly();

    public static ImageBlobEntity Create (BlobSubscriptionBaseEntity blob, List<TagEnity> tags)
	{
		var image = Create<ImageBlobEntity>(blob)!;

		image.Update(tags);

		return image;
	}

	public void Update (BlobSubscriptionBaseEntity blob, string displayName, string description, List<TagEnity> tags)
	{

		if (blob is not null) Update(blob);

		DisplayName = displayName;
		Description = description;

		Update(tags);
    }

    private void Update(List<TagEnity> tags)
    {
        var tagsToRemove = Tags.Where(existingTag => !tags.Any(newTag => newTag.Name == existingTag.Name)).ToList();
        foreach (var tag in tagsToRemove)
        {
            _tags.Remove(tag);
        }

        var tagsToAdd = tags.Where(newTag => !Tags.Any(existingTag => existingTag.Name == newTag.Name)).ToList();


        tags.ForEach(e => _tags.Add(e));
    }
}
