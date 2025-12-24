using lrembecki.core.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface IAdvantageService : ICrudService<AdvantageDto, AdvantageVM>;
internal sealed class AdvantageService(IUnitOfWork uow) : BaseCrudService<AdvantageEntity, AdvantageVM, AdvantageDto>(uow), IAdvantageService;
