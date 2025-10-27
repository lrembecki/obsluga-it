namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record ActorVM(
    Guid Id,
    string? Firstname,
    string? Lastname,
    List<ContactVM> Contacts
);
