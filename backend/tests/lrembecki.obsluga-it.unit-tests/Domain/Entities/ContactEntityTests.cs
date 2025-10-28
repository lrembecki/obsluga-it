using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class ContactEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var entity = ContactEntity.Create(id, "john.doe@example.com", "+1234567890");
        Assert.Equal(id, entity.Id);
        Assert.Equal("john.doe@example.com", entity.Email.Address);
        Assert.Equal("+1234567890", entity.Phone.Number);
    }

    [Fact]
    public void Update_ChangesEmailAndPhone()
    {
        var entity = ContactEntity.Create(Guid.NewGuid(), "john.doe@example.com", "+1234567890");
        entity.Update("alice@example.com", "+10987654321");
        Assert.Equal("alice@example.com", entity.Email.Address);
        Assert.Equal("+10987654321", entity.Phone.Number);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-email")]
    public void Create_InvalidEmail_Throws(string? email)
    {
        Assert.Throws<ArgumentException>(() => ContactEntity.Create(Guid.NewGuid(), email!, "+1234567890"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")] // too short / invalid format
    public void Create_InvalidPhone_Throws(string? phone)
    {
        Assert.Throws<ArgumentException>(() => ContactEntity.Create(Guid.NewGuid(), "john.doe@example.com", phone!));
    }
}
