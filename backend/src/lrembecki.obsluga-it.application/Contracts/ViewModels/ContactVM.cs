namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record ContactVM(
    Guid Id,
    string Email,
    string Phone
);