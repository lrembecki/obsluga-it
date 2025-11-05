using System.Security.Claims;

namespace lrembecki.core.Helpers
{
    public interface IJwtTokenFactory
    {
        string CreateToken(ClaimsIdentity identity, IEnumerable<string> permissionVMs, DateTime created, DateTime expires);
        ClaimsIdentity GetClaimsIdentity(Guid subscriptionId, Guid userId, string email, string[] permissions, DateTime created, DateTime expires);
    }
}
