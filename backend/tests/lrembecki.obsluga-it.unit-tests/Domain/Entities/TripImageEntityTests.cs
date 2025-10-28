using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripImageEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var blob = CreateBlob("img.png");
        var image = ImageBlobEntity.Create(blob, []);
        var entity = TripImageEntity.Create(true, 1, image);
        Assert.True(entity.IsMain);
        Assert.Equal(1, entity.Order);
        Assert.Equal(image, entity.Image);
    }

    [Fact]
    public void Update_ChangesProperties()
    {
        var image = ImageBlobEntity.Create(CreateBlob("a.png"), []);
        var entity = TripImageEntity.Create(false, 2, image);
        var image2 = ImageBlobEntity.Create(CreateBlob("b.png"), []);
        entity.Update(true, 5, image2);
        Assert.True(entity.IsMain);
        Assert.Equal(5, entity.Order);
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
