using lrembecki.core.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface IAboutUsService : ICrudService<AboutUsDto, AboutUsVM>;
internal sealed class AboutUsService(IUnitOfWork uow) : BaseCrudService<AboutUsEntity, AboutUsVM, AboutUsDto>(uow), IAboutUsService;