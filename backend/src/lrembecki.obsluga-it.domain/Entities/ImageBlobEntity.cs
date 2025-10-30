namespace lrembecki.obsluga_it.domain.Entities;

internal class ImageBlobEntity : BlobBaseEntity
{
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; } = default!;
    public long? Width { get; private set; } = default!;
    public long? Height { get; private set; } = default!;

    public List<TagEntity> Tags { get; private set; } = [];

    public void Update(string? displayName, string? description, long? width, long? height, List<TagEntity> tags)
    {

        DisplayName = displayName;
        Description = description;
        Width = width;
        Height = height;

        Update(tags);
    }

    private void Update(List<TagEntity> tags)
    {
        var tagsToRemove = Tags.Where(existingTag => !tags.Any(newTag => newTag.Name == existingTag.Name)).ToList();
        foreach (var tag in tagsToRemove)
        {
            Tags.Remove(tag);
        }

        var tagsToAdd = tags.Where(newTag => !Tags.Any(existingTag => existingTag.Name == newTag.Name)).ToList();


        tagsToAdd.ForEach(e => Tags.Add(e));
    }
}
