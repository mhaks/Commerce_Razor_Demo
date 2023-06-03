using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CommerceRazorDemo.Pages.Customers
{
    [Authorize(Roles = "ADMIN")]
    public class IndexModel : CommerceDemoPageModel
    {

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        public IList<ApplicationUser> Customer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string NameSort { get; set; } = String.Empty;

        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            if (_context == null)
                return NotFound();



            var customerQuery = from c in _context.Users select c;

            if (!String.IsNullOrEmpty(SearchString))
            {
                customerQuery = customerQuery.Where(c => c.FirstName.Contains(SearchString) || c.LastName.Contains(SearchString));
            }

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            switch(sortOrder)
            {
                case "name_desc":
                    customerQuery = customerQuery.OrderByDescending(c => c.LastName);
                    break;

                default:
                    customerQuery = customerQuery.OrderBy(c => c.LastName);
                    break;
            }

            Customer = await customerQuery.AsNoTracking().Include(c => c.StateLocation).ToListAsync();

            return Page();
        }
    }
}
