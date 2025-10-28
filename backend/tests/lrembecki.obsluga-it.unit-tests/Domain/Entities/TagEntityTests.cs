using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TagEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var entity = TagEnity.Create(id, "Tag");
        Assert.Equal(id, entity.Id);
        Assert.Equal("Tag", entity.Name);
    }

    [Fact]
    public void Update_ChangesName()
    {
        var entity = TagEnity.Create(Guid.NewGuid(), "Old");
        entity.Update("New");
        Assert.Equal("New", entity.Name);
    }
}
