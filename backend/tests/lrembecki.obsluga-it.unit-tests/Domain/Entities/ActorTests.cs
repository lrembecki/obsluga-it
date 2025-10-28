using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class ActorTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var actor = ActorEntity.Create(id, "Alice", "Smith");

        // Assert
        Assert.Equal(id, actor.Id);
        Assert.Equal("Alice", actor.Firstname);
        Assert.Equal("Smith", actor.Lastname);
        Assert.Empty(actor.Contacts);
        Assert.Empty(actor.DomainEvents); // no domain event raised currently
    }

    [Fact]
    public void Update_ChangesFirstnameAndLastname()
    {
        var actor = ActorEntity.Create(Guid.NewGuid(), "Alice", "Smith");

        // Act
        actor.Update("Bob", "Jones");

        // Assert
        Assert.Equal("Bob", actor.Firstname);
        Assert.Equal("Jones", actor.Lastname);
    }

    [Fact]
    public void AddAndRemoveContact_ModifiesContactsCollection()
    {
        var actor = ActorEntity.Create(Guid.NewGuid(), "Alice", "Smith");
        var contact = ContactEntity.Create(Guid.NewGuid(), "alice@example.com", "+48123123123");

        // Act - add
        actor.AddContact(contact);

        // Assert add
        Assert.Single(actor.Contacts);
        Assert.Contains(contact, actor.Contacts);

        // Act - remove
        actor.RemoveContact(contact);

        // Assert remove
        Assert.Empty(actor.Contacts);
    }

    [Fact]
    public void Create_WithNullNames_AllowsNulls()
    {
        var id = Guid.NewGuid();
        var actor = ActorEntity.Create(id, null, null);

        Assert.Equal(id, actor.Id);
        Assert.Null(actor.Firstname);
        Assert.Null(actor.Lastname);
    }
}
