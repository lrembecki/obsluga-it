using lrembecki.core.settings.Emails;
using lrembecki.core.settings.EmailTemplates;

namespace lrembecki.core.settings.Notifications;

public record  EmailNotificationVM(
    Guid EmailId,
    EmailWithPasswordVM Email,
    Guid EmailTemplateId,
    EmailTemplateVM EmailTemplate
)
{
    internal static EmailNotificationVM Map(EmailNotificationEntity entity)
    {
        if (entity == null) return null!;

        return new EmailNotificationVM(
            entity.EmailId,
            EmailWithPasswordVM.Map(entity.Email),
            entity.EmailTemplateId,
            EmailTemplateVM.Map(entity.EmailTemplate)
        );
    }
}
