using ContactManagementWebApp.Data;
using ContactManagementWebApp.Models.Contact;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementWebApp.Repositories.Contact
{
    public class ContactRepository : BaseRepository<ContactEntity> , IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
