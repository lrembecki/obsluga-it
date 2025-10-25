using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Abstractions;

public interface ITokenHelper
{
    string CreateToken(
        UserVM userVM,
        IEnumerable<string> permissionVMs,
        SubscriptionVM subscriptionVM,
        DateTime created,
        DateTime expires);
}
