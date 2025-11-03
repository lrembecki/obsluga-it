using lrembecki.core;
using lrembecki.core.settings.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace lrembecki.presentation.settings;

public static class ContactsEndpoint
{
    internal static IEndpointRouteBuilder MapContacts(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/contacts", async (
            [FromServices] IContactService contactService,
            CancellationToken ct
        ) => (await contactService.GetAllAsync(ct)).ToServiceCallResult())
            .WithTags("Get");

        endpoints.MapPost("/contacts", async (
            [FromBody] ContactDto model,
            [FromServices] IContactService contactService,
            CancellationToken ct
        ) => (await contactService.CreateAsync(model, ct)).ToServiceCallResult())
            .WithTags("Post");

        endpoints.MapPut("/contacts/{contactId:guid}", async (
            [FromRoute] Guid contactId,
            [FromBody] ContactDto model,
            [FromServices] IContactService contactService,
            CancellationToken ct
        ) => (await contactService.UpdateAsync(contactId, model, ct)).ToServiceCallResult())
            .WithTags("Put");

        endpoints.MapDelete("/contacts/{contactId:guid}", (
            [FromRoute] Guid contactId,
            [FromServices] IContactService contactService,
            CancellationToken ct
        ) => contactService.DeleteAsync(contactId, ct).ToServiceCallResult())
            .WithTags("Delete");

        return endpoints;
    }
}