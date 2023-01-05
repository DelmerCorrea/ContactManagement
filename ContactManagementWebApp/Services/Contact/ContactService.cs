using ContactManagementWebApp.Models.Contact;
using ContactManagementWebApp.Repositories.Contact;

namespace ContactManagementWebApp.Services.Contact
{
    public class ContactService : ServiceBase<ContactEntity>, IContactService
    {

        public ContactService(IContactRepository repository) : base(repository)
        {
        }
    }
}
