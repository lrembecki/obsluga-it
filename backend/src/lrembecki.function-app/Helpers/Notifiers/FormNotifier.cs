using lrembecki.core.Events;
using lrembecki.core.forms.Defaults;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.core.settings.Services;

namespace lrembecki.function_app.Helpers.Notifiers;

internal sealed class FormNotifier(
    IFormService forms, 
    IFormDefinitionService formDefinitions,
    IEmailService emails, 
    IEmailSender emailSender,
    IBlobHelper blob) : IEmailNotifier
{
    public async Task Notify(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var form = await forms.GetByIdAsync(domainEvent.ExternalId, ct);
        var definition = await formDefinitions.GetByIdAsync(form.FormDefinitionId, ct);

        var settingsVMs = await emails.GetAllAsync(ct);

        var settings = definition.Notification!.Email.Email;
        var template = definition.Notification!.Email.EmailTemplate!;

        var stream = await blob.DownloadBlobAsync("storage", template.TemplateHtml.BlobPath!, ct);
        using var reader = new StreamReader(stream);
        var templateHtml = await reader.ReadToEndAsync(ct);

        var mappingGroup = definition.EmailNotificationFieldMapping
            .GroupBy(e => e.EmailTemplateFieldName, e => form.Fields[e.FormDefinitionFieldName]);
        var mapping = mappingGroup
            .ToDictionary(e => e.Key);

        foreach (var key in mappingGroup)
        foreach (var value in key)
        { 
            templateHtml = templateHtml.Replace(key.Key, value!);
        }

        if (mapping.ContainsKey(EmailTemplateFieldDefaults.MailTo))
        {
            var mailTo = mapping[EmailTemplateFieldDefaults.MailTo].ToList();
            var bcc = mapping.ContainsKey(EmailTemplateFieldDefaults.MailBcc)
                ? mapping[EmailTemplateFieldDefaults.MailBcc].ToList()
                : [];

            await emailSender.SendEmailAsync(settings, new(
                To: [string.Join(";", mailTo)],
                CC: [],
                BCC: [..bcc, "lrembecki@hotmail.com"], // kontakt@trotamundos.pl 
                Subject: "(Don't reply) Kontakt trotamundos.pl",
                Body: templateHtml
            ));
        }
    }
}
