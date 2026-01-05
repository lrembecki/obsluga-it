using lrembecki.core.Services;

namespace lrembecki.core.settings.Website;


public interface IWebsiteService : ICrudService<WebsiteDto, WebsiteVM>;
internal sealed class WebsiteService(IUnitOfWork uow) : BaseCrudService<WebsiteEntity, WebsiteVM, WebsiteDto>(uow), IWebsiteService;
