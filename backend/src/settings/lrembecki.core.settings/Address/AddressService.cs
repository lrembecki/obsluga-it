using lrembecki.core.Services;

namespace lrembecki.core.settings.Address;


public interface IAddressService : ICrudService<AddressDto, AddressVM>;
internal sealed class AddressService(IUnitOfWork uow) : BaseCrudService<AddressEntity, AddressVM, AddressDto>(uow), IAddressService;