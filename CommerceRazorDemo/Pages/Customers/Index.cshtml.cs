using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Pages.Customers
{
    public class IndexModel : CommerceDemoPageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<CommerceDemoPageModel> _logger;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        public IList<Customer> Customer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = String.Empty;

        public string NameSort { get; set; } = String.Empty;

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.Customer != null)
            {
                var customerQuery = from c in _context.Customer select c;

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
            }

        }
    }
}
