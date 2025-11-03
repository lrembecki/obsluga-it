using lrembecki.core.Services;

namespace lrembecki.core.settings.Entities;

public sealed class ContactService (IUnitOfWork uow) : BaseCrudService<ContactEntity, ContactVM, ContactDto>(uow), IContactService;
