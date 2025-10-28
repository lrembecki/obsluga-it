using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class AdvantageEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var entity = AdvantageEntity.Create(id, "Title", "Content");
        Assert.Equal(id, entity.Id);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("Content", entity.Content);
    }

    [Fact]
    public void Update_ChangesProperties()
    {
        var entity = AdvantageEntity.Create(Guid.NewGuid(), "T1", "C1");
        entity.Update("T2", "C2");
        Assert.Equal("T2", entity.Title);
        Assert.Equal("C2", entity.Content);
    }
}
