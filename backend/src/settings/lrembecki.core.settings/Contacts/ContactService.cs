using lrembecki.core.Services;

namespace lrembecki.core.settings.Contacts;

internal sealed class ContactService (IUnitOfWork uow) : BaseCrudService<ContactEntity, ContactVM, ContactDto>(uow), IContactService;
