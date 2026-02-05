using lrembecki.core.settings.Companies;
using lrembecki.core.settings.Contacts;

namespace lrembecki.function_app.Helpers.Publishers;

internal record PublishCompanyVM(CompanyVM original, List<ContactVM> phoneContacts, List<ContactVM> emailContacts) : CompanyVM(original)
{
    public List<ContactVM> Phones { get; set; } = phoneContacts;
    public List<ContactVM> Emails { get; set; } = emailContacts;
}
