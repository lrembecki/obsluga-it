using DomainFile = lrembecki.obsluga_it.domain.Entities.File;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class FileTests
{
    [Fact]
    public void Create_SetsFileId_AndDefaults()
    {
        var id = Guid.NewGuid();
        var file = DomainFile.Create(id);

        Assert.Equal(id, file.FileId);
        Assert.Null(file.Description);
        Assert.Null(file.DisplayName);
        Assert.Equal(Guid.Empty, file.SubscriptionId);
        Assert.Null(file.GroupId);
        Assert.Null(file.Group);
    }

    [Fact]
    public void Update_DisplayNameAndPosition_SetsValues()
    {
        var file = DomainFile.Create(Guid.NewGuid());
        file.Update("Display", 5);

        Assert.Equal("Display", file.DisplayName);
        Assert.Equal(5, file.Position);
    }

    [Theory]
    [InlineData("Some description", "Some description")]
    [InlineData(" ", null)]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void Update_MainFields_NormalizesDescription(string? input, string? expected)
    {
        var file = DomainFile.Create(Guid.NewGuid());
        file.Update("name.txt", input ?? string.Empty, "/blob/path", "https://url", 123L);

        Assert.Equal("name.txt", file.Name);
        Assert.Equal(expected, file.Description);
        Assert.Equal("/blob/path", file.BlobPath);
        Assert.Equal("https://url", file.Url);
        Assert.Equal(123L, file.Filesize);
    }

    [Fact]
    public void Update_Group_SetsGroupAndGroupId()
    {
        var file = DomainFile.Create(Guid.NewGuid());
        var group = FileGroup.Create(Guid.NewGuid(), "GroupA");

        file.Update(group);

        Assert.Equal(group.FileGroupId, file.GroupId);
        Assert.Equal(group, file.Group);
    }

    [Fact]
    public void Update_Group_Null_ClearsGroup()
    {
        var file = DomainFile.Create(Guid.NewGuid());
        var group = FileGroup.Create(Guid.NewGuid(), "GroupA");
        file.Update(group);

        file.Update((FileGroup?)null);

        Assert.Null(file.GroupId);
        Assert.Null(file.Group);
    }
}
