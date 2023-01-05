using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactManagementWebApp.Data;
using ContactManagementWebApp.Models.Contact;
using ContactManagementWebApp.Services.Contact;
using Microsoft.AspNetCore.Authorization;

namespace ContactManagementWebApp.Pages.Contact
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IContactService _service;

        public CreateModel(IContactService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContactEntity ContactEntity { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.AddAsync(ContactEntity);

            return RedirectToPage("./Index");
        }
    }
}
