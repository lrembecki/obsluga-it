using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record ContactVM(
    Guid Id,
    string Email,
    string Phone
)
{
    public static ContactVM MapFromDomainEntity(ContactEntity contactEntity)
    {
        if (contactEntity == null)
        {
            return null!;
        }

        return new ContactVM(
            contactEntity.Id,
            contactEntity.Email,
            contactEntity.Phone
        );
    }
}