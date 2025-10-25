using System.Security.Claims;

namespace lrembecki.obsluga_it.infrastructure;

internal class CustomClaimTypes
{
	public const string SubscriptionId = "SubscriptionId";
	public const string AccountId = "AccountId";
	public const string UserId = "UserId";
	public const string Role = ClaimTypes.Role;
	public const string Created = "Created";
	public const string Expires = ClaimTypes.Expiration;
	public const string Email = ClaimTypes.Email;
	public const string NameIdentifier = ClaimTypes.NameIdentifier;
	public const string Subject = "sub";
}
