using lrembecki.core.Services;

namespace lrembecki.core.settings.Ftps;

public interface IFtpService : ICrudService<FtpDto, FtpVM>;
public sealed class FtpService(IUnitOfWork uow) : BaseCrudService<FtpEntity, FtpVM, FtpDto>(uow);
