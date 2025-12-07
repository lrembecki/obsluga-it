using lrembecki.core.Services;

namespace lrembecki.core.notification.EmailNotifications;

public interface IEmailNotificationService : ICrudService<EmailNotificationDto, EmailNotificationVM>;
internal sealed class EmailNotificationService(
    IUnitOfWork uow
) : BaseCrudService<EmailNotification, EmailNotificationVM, EmailNotificationDto>(uow), IEmailNotificationService;