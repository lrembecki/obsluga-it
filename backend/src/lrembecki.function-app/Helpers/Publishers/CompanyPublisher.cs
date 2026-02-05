using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.settings.Companies;
using lrembecki.core.settings.Contacts;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class CompanyPublisher(
    ICompanyService companies,
    IContactService contacts,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var dataVMs = await companies.GetAllAsync(ct);
        var contactVMs = await contacts.GetAllAsync(ct);

        var dataVM = dataVMs
            .Select(c => new PublishCompanyVM(
                c, 
                contactVMs.Where(ct => c.PhoneContact.Contains(ct.Id)).ToList(),
                contactVMs.Where(ct => c.EmailContact.Contains(ct.Id)).ToList()))
            .ToList();


        await uploadHelper.Upload(dataVM, domainEvent.SubscriptionId.ToString(), "settings", "companies");
    }
}