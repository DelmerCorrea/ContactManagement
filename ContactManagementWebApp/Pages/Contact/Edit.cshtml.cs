using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManagementWebApp.Data;
using ContactManagementWebApp.Models.Contact;
using ContactManagementWebApp.Services.Contact;
using Microsoft.AspNetCore.Authorization;

namespace ContactManagementWebApp.Pages.Contact
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IContactService _service;

        public EditModel(IContactService service)
        {
            _service = service;
        }

        [BindProperty]
        public ContactEntity ContactEntity { get; set; } = default!;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _service.UpdateAsync(ContactEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactEntityExists(ContactEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactEntityExists(int id)
        {
          return _service.GetByIdAsync(id) != null;
        }
    }
}
