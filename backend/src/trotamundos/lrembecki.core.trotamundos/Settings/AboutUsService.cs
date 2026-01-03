using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Settings;

public interface IAboutUsService : ICrudService<AboutUsDto, AboutUsVM>;
internal sealed class AboutUsService(IUnitOfWork uow) : BaseCrudService<AboutUsEntity, AboutUsVM, AboutUsDto>(uow), IAboutUsService;