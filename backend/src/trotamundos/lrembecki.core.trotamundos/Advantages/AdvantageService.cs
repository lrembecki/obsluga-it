using lrembecki.core.Services;

namespace lrembecki.core.trotamundos.Advantages;

public interface IAdvantageService : ICrudService<AdvantageDto, AdvantageVM>;
internal sealed class AdvantageService(IUnitOfWork uow) : BaseCrudService<AdvantageEntity, AdvantageVM, AdvantageDto>(uow), IAdvantageService;
