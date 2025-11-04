using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;

namespace lrembecki.core.settings.Services;

public sealed class ContactService (IUnitOfWork uow) : BaseCrudService<ContactEntity, ContactVM, ContactDto>(uow), IContactService;
