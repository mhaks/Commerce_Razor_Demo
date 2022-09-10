using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public CreateModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StateLocationId"] = new SelectList(_context.StateLocation, "Id", "Abbreviation");
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Customer == null || Customer == null)
            {
                return Page();
            }

            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
