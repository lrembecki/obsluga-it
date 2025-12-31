using lrembecki.core.Events;
using lrembecki.core.forms.Defaults;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.core.settings.Services;
using lrembecki.core;

namespace lrembecki.function_app.Helpers.Notifiers;

internal sealed class FormNotifier(
    IFormService forms, 
    IFormDefinitionService formDefinitions,
    IEmailService emails, 
    IContactService contacts,
    IEmailSender emailSender,
    IBlobHelper blob) : IEmailNotifier
{
    public async Task Notify(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var form = await forms.GetByIdAsync(domainEvent.ExternalId, ct);
        if (form is null) return;

        var definition = await formDefinitions.GetByIdAsync(form.FormDefinitionId, ct);
        var settingsVMs = await emails.GetAllAsync(ct);
        var contactsVMs = await contacts.GetAllAsync(ct);

        var settings = definition.Notification!.Email.Email;
        var template = definition.Notification!.Email.EmailTemplate!;
        var templateHtml = await blob.ReadAllTextAsync("storage", template.TemplateHtml.BlobPath!, ct);
        var mappingGroup = definition.EmailNotificationFieldMapping
            .GroupBy(e => e.EmailTemplateFieldName, e => form.Fields[e.FormDefinitionFieldName]);
        var mapping = mappingGroup
            .ToDictionary(e => e.Key);

        foreach (var key in mappingGroup)
        foreach (var value in key)
        {
            var htmlValue = string.Join(Environment.NewLine, value.Split(Environment.NewLine).Select(e => $"<p>{e}</p>"));
            templateHtml = templateHtml.Replace($"{{{{{key.Key}}}}}", htmlValue);
        }

        if (mapping.ContainsKey(EmailTemplateFieldDefaults.MailTo))
        {
            contactsVMs = contactsVMs.Where(e => e.IsActive).ToList();

            await emailSender.SendEmailAsync(settings, new(
                To: [.. GetRecipientList(contactsVMs, mapping, EmailTemplateFieldDefaults.MailTo, template.Contacts_to)],
                CC: [.. GetRecipientList(contactsVMs, mapping, EmailTemplateFieldDefaults.MailCC, template.Contacts_cc)],
                BCC: [.. GetRecipientList(contactsVMs, mapping, EmailTemplateFieldDefaults.MailBcc, template.Contacts_bcc)],
                Subject: template.Subject,
                Body: templateHtml
            ));
        }
    }

    private static List<string> GetRecipientList(List<core.settings.ViewModels.ContactVM> contactsVMs, Dictionary<string, IGrouping<string, string>> mapping, string mappingKey, List<Guid> contactIds)
    {
        var to = contactIds
            .Select(e => contactsVMs.Find(y => y.Id == e))
            .Select(e => e?.Email!)
            .Where(e => !string.IsNullOrEmpty(e))
            .ToList();

        if (mapping.ContainsKey(mappingKey))
        {
            to = [.. to, .. mapping[mappingKey]];
        }

        return to;
    }
}
