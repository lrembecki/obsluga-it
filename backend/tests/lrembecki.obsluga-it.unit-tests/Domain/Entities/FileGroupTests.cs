using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class FileGroupTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var fg = FileGroup.Create(id, "Group1");

        Assert.Equal(id, fg.FileGroupId);
        Assert.Equal("Group1", fg.Name);
    }
}
