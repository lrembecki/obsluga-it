using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.ViewModels;

namespace lrembecki.core.settings.Services;

public interface IEmailService : ICrudService<EmailDto, EmailVM>;
