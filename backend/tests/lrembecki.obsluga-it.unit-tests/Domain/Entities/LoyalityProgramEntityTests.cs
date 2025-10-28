using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class LoyalityProgramEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var blob = CreateBlob("img.png");
        var image = ImageBlobEntity.Create(blob, []);
        var entity = LoyalityProgramEntity.Create(id, "Name", "Title", "Desc", image);
        Assert.Equal(id, entity.Id);
        Assert.Equal("Name", entity.Name);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("Desc", entity.Description);
        Assert.Equal(image, entity.Image);
    }

    [Fact]
    public void Update_ChangesValues()
    {
        var blob = CreateBlob("img.png");
        var image = ImageBlobEntity.Create(blob, []);
        var entity = LoyalityProgramEntity.Create(Guid.NewGuid(), "N1", "T1", "D1", image);
        var image2 = ImageBlobEntity.Create(CreateBlob("i2.png"), []);
        entity.Update("N2", "T2", "D2", image2);
        Assert.Equal("N2", entity.Name);
        Assert.Equal("T2", entity.Title);
        Assert.Equal("D2", entity.Description);
        Assert.Equal(image2, entity.Image);
    }

    private static BlobBaseEntity CreateBlob(string filename)
    {
        var blob = Activator.CreateInstance(typeof(BlobBaseEntity), true)!;
        blob.GetType().GetProperty("Id")!.SetValue(blob, Guid.NewGuid());
        blob.GetType().GetProperty("Filename")!.SetValue(blob, filename);
        blob.GetType().GetProperty("BlobUrl")!.SetValue(blob, "https://blob");
        blob.GetType().GetProperty("BlobPath")!.SetValue(blob, "/p");
        blob.GetType().GetProperty("Size")!.SetValue(blob, 1L);
        return (BlobBaseEntity)blob;
    }
}
