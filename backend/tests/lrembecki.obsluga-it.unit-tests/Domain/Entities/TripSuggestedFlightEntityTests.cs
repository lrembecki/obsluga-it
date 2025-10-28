using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripSuggestedFlightEntityTests
{
    [Fact]
    public void Create_SetsFields()
    {
        var image = ImageBlobEntity.Create(CreateBlob("img.png"), []);
        var entity = TripSuggestedFlightEntity.Create(1, image);
        Assert.Equal(1, entity.Order);
        Assert.Equal(image, entity.Image);
    }

    [Fact]
    public void Update_ModifiesFields()
    {
        var image = ImageBlobEntity.Create(CreateBlob("a.png"), []);
        var entity = TripSuggestedFlightEntity.Create(1, image);
        var image2 = ImageBlobEntity.Create(CreateBlob("b.png"), []);
        entity.Update(2, image2);
        Assert.Equal(2, entity.Order);
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
