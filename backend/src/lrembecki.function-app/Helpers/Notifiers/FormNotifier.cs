using lrembecki.core.Events;
using lrembecki.core.forms.Forms;
using lrembecki.core.Services;
using lrembecki.core.settings.Services;

namespace lrembecki.function_app.Helpers.Notifiers;

internal sealed class FormNotifier(IFormService forms, IEmailService emails, IEmailSender emailSender) : IEmailNotifier
{
    public async Task Notify(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var form = await forms.GetByIdAsync(domainEvent.ExternalId, ct);
        var settingsVMs = await emails.GetAllAsync(ct);
        var settings = settingsVMs.First();

        await emailSender.SendEmailAsync(settings, new(
            To: ["lrembecki@hotmail.com"], 
            CC: [], 
            BCC: ["lrembecki@hotmail.com"], // kontakt@trotamundos.pl 
            Subject: "(Don't reply) Kontakt trotamundos.pl", 
            Body: ""
        ));
    }
}
