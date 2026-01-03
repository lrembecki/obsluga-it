using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Highlights;

public interface IHighlightService : ICrudService<HighlightDto, HighlightVM>;
internal sealed class HighlightService(IUnitOfWork uow) : BaseCrudService<HighlightEntity, HighlightVM, HighlightDto>(uow), IHighlightService;