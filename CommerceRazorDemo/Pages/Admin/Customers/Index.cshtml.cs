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
using Microsoft.AspNetCore.Identity;

namespace CommerceRazorDemo.Pages.Customers
{
    [Authorize(Roles = "ADMIN")]
    public class IndexModel : CommerceDemoPageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
            : base(context, logger)
        {
            _userManager = userManager;
        }

        public IList<ApplicationUser> Customer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string NameSort { get; set; } = String.Empty;

        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            if (_context == null)
                return NotFound();


            var customers = await _userManager.GetUsersInRoleAsync("CUSTOMER");

            var customerQuery = from c in customers 
                                join st in _context.StateLocation on c.StateLocationId equals st.Id
                                select c;


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

            Customer = customerQuery.ToList();

            return Page();
        }
    }
}
