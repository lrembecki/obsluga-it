using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class FileBlobEntityTests
{
    [Fact]
    public void Create_SetsPropertiesFromBlobAndArguments()
    {
        var blob = CreateBlob();
        var group = Activator.CreateInstance(typeof(FileGroupEntity), true)!;
        var fileId = Guid.NewGuid();
        var entity = FileBlobEntity.Create(fileId, "Display", "Desc", 5, (FileGroupEntity)group, blob);
        Assert.Equal(fileId, entity.Id);
        Assert.Equal("Display", entity.DisplayName);
        Assert.Equal("Desc", entity.Description);
        Assert.Equal(5, entity.Position);
        Assert.Equal(group.GetType().GetProperty("Id")!.GetValue(group), entity.GroupId);
        Assert.Equal(blob.Filename, entity.Filename);
    }

    [Fact]
    public void Update_ChangesPropertiesAndBlob()
    {
        var blob = CreateBlob();
        var entity = FileBlobEntity.Create(Guid.NewGuid(), "D1", "Desc1", 1, null!, blob);
        var newBlob = CreateBlob("new.txt", "https://new", "/n", 200);
        entity.Update("Desc2", "D2", 3, null, newBlob);
        Assert.Equal("Desc2", entity.Description);
        Assert.Equal("D2", entity.DisplayName);
        Assert.Equal(3, entity.Position);
        Assert.Equal("new.txt", entity.Filename);
    }

    private static BlobBaseEntity CreateBlob(string filename = "file.txt", string url = "https://blob", string path = "/p", long size = 100)
    {
        var blob = Activator.CreateInstance(typeof(BlobBaseEntity), true)!;
        blob.GetType().GetProperty("Id")!.SetValue(blob, Guid.NewGuid());
        blob.GetType().GetProperty("Filename")!.SetValue(blob, filename);
        blob.GetType().GetProperty("BlobUrl")!.SetValue(blob, url);
        blob.GetType().GetProperty("BlobPath")!.SetValue(blob, path);
        blob.GetType().GetProperty("Size")!.SetValue(blob, size);
        return (BlobBaseEntity)blob;
    }
}
