using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class HighlightEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var entity = HighlightEntity.Create(id, "Title", "Icon");
        Assert.Equal(id, entity.Id);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("Icon", entity.Icon);
    }

    [Fact]
    public void Update_ChangesProperties()
    {
        var entity = HighlightEntity.Create(Guid.NewGuid(), "T1", "I1");
        entity.Update("T2", "I2");
        Assert.Equal("T2", entity.Title);
        Assert.Equal("I2", entity.Icon);
    }
}
