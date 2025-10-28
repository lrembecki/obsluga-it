using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.infrastructure.Abstractions;
using lrembecki.obsluga_it.infrastructure.Extensions;

namespace lrembecki.obsluga_it.api.Endpoints;

public sealed class ContactEndpoints : IEndpointModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/contacts")
        .RequireAuthorization(AuthenticationExtensions.InternalJwtPolicy)
        .WithTags("Contacts")
        .MapGet("/", (ISender sender) => sender.SendAsync(new ContactsGetQuery()));
    }
}
