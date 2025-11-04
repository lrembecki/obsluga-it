using lrembecki.core.settings.Entities;

namespace lrembecki.core.settings.ViewModels;

public record ContactVM(
    Guid Id, 
    string Name, 
    bool IsActive, 
    string? Position,
    string? Email,
    string? Phone)
{
    public static ContactVM Map(ContactEntity contact)
    {
        if (contact == null) return null!;

        return new (
            Id: contact.Id,
            Name: contact.Name,
            IsActive: contact.IsActive,
            Position: contact.Position,
            Email: contact.Email,
            Phone: contact.Phone
        );
    }
}
