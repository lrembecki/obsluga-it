using lrembecki.core.Services;

namespace lrembecki.core.settings.Emails;

public interface IEmailService : ICrudService<EmailDto, EmailWithPasswordVM>;
