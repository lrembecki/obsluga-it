using lrembecki.core.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface IHighlightService : ICrudService<HighlightDto, HighlightVM>;
internal sealed class HighlightService(IUnitOfWork uow) : BaseCrudService<HighlightEntity, HighlightVM, HighlightDto>(uow), IHighlightService;