using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Abstractions;

public interface IAuthenticationService
{
    Task<AccountVM> SignInAsync(string email, Guid? subscriptionId, CancellationToken cancellationToken = default);
}
