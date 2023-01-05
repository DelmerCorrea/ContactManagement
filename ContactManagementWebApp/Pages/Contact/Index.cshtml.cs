using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactManagementWebApp.Data;
using ContactManagementWebApp.Models.Contact;
using ContactManagementWebApp.Services.Contact;

namespace ContactManagementWebApp.Pages.Contact
{
    public class IndexModel : PageModel
    {
        private readonly IContactService _service;

        public IndexModel(IContactService service)
        {
            _service = service;
        }

        public IList<ContactEntity> ContactEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            IEnumerable<ContactEntity> contacts = await _service.GetAllAsync();

            ContactEntity = contacts?.ToList() ?? new List<ContactEntity>();
        }
    }
}
