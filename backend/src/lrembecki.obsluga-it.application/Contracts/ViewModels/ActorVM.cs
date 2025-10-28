using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record ActorVM(
    Guid? Id,
    string? Firstname,
    string? Lastname,
    List<ContactVM> Contacts
)
{
    internal static ActorVM MapFromDomainEntity(ActorEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new ActorVM(
            entity.Id,
            entity.Firstname,
            entity.Lastname,
            entity.Contacts.Select(ContactVM.MapFromDomainEntity).ToList()
        );
    }
}

