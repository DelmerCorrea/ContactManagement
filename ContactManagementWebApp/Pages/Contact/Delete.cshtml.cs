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
    public class DeleteModel : PageModel
    {
        private readonly IContactService _service;

        public DeleteModel(IContactService service)
        {
            _service = service;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactentity = await _service.GetByIdAsync(id);

            if (contactentity != null)
            {
                ContactEntity = contactentity;
                await _service.RemoveAsync(ContactEntity);
            }

            return RedirectToPage("./Index");
        }
    }
}
