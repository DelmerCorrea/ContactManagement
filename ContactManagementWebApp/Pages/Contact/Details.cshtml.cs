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
using Microsoft.AspNetCore.Authorization;

namespace ContactManagementWebApp.Pages.Contact
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IContactService _service;

        public DetailsModel(IContactService service)
        {
            _service = service;
        }

      public ContactEntity ContactEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactentity = await _service.GetByIdAsync(id);

            if (contactentity == null)
            {
                return NotFound();
            }
                
            ContactEntity = contactentity;
            
            return Page();
        }
    }
}
