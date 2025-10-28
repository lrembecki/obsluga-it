using lrembecki.obsluga_it.application.Contracts.ViewModels;
using System.Security.Claims;

namespace lrembecki.obsluga_it.application.Abstractions.Factories;

public interface IJwtTokenFactory
{
    string CreateToken(
        UserVM userVM,
        IEnumerable<string> permissionVMs,
        SubscriptionVM subscriptionVM,
        DateTime created,
        DateTime expires);

    ClaimsIdentity GetClaimsIdentity(
        UserVM userVM, 
        SubscriptionVM subscriptionVM, 
        string[] permissions, 
        DateTime created, 
        DateTime expires
    );
}
