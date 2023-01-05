using ContactManagementWebApp.Models.Contact;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementWebApp.Repositories.Contact
{
    public class ContactRepository : BaseRepository<ContactEntity> , IContactRepository
    {
        protected ContactRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
