using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class ImageBlobEntityTests
{
    [Fact]
    public void Create_CopiesBlobAndSetsTags()
    {
        var blob = CreateBlob("img.png");
        var tags = new List<TagEnity> { TagEnity.Create(Guid.NewGuid(), "t1"), TagEnity.Create(Guid.NewGuid(), "t2") };
        var entity = ImageBlobEntity.Create(blob, tags);
        Assert.Equal(blob.Id, entity.Id);
        Assert.Equal(blob.Filename, entity.Filename);
        Assert.Equal(2, entity.Tags.Count);
    }

    [Fact]
    public void Update_ReplacesDisplayDescriptionAndTags()
    {
        var blob = CreateBlob("img.png");
        var tags = new List<TagEnity> { TagEnity.Create(Guid.NewGuid(), "t1") };
        var entity = ImageBlobEntity.Create(blob, tags);
        var newBlob = CreateBlob("img2.png");
        var newTags = new List<TagEnity> { TagEnity.Create(Guid.NewGuid(), "t2"), TagEnity.Create(Guid.NewGuid(), "t3") };
        entity.Update(newBlob, "Disp", "Desc", newTags);
        Assert.Equal("Disp", entity.DisplayName);
        Assert.Equal("Desc", entity.Description);
        Assert.Equal("img2.png", entity.Filename);
        Assert.Contains(entity.Tags, t => t.Name == "t2");
        Assert.Contains(entity.Tags, t => t.Name == "t3");
    }

    private static BlobBaseEntity CreateBlob(string filename = "file.txt")
    {
        var blob = Activator.CreateInstance(typeof(BlobBaseEntity), true)!;
        blob.GetType().GetProperty("Id")!.SetValue(blob, Guid.NewGuid());
        blob.GetType().GetProperty("Filename")!.SetValue(blob, filename);
        blob.GetType().GetProperty("BlobUrl")!.SetValue(blob, "https://blob");
        blob.GetType().GetProperty("BlobPath")!.SetValue(blob, "/p");
        blob.GetType().GetProperty("Size")!.SetValue(blob, 10L);
        return (BlobBaseEntity)blob;
    }
}
