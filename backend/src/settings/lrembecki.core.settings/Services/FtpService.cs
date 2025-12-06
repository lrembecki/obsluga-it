using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;

namespace lrembecki.core.settings.Services;

public interface IFtpService : ICrudService<FtpDto, FtpVM>;
public sealed class FtpService(IUnitOfWork uow) : BaseCrudService<FtpEntity, FtpVM, FtpDto>(uow);
