using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using System.ComponentModel;

namespace CommerceRazorDemo.Pages.OrderStatuses
{
    public class IndexModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        public IList<OrderStatus> OrderStatus { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string? NameSort { get; set; } = string.Empty;  

        public async Task OnGetAsync(string? sortOrder)
        {
            if (_context.OrderStatus != null)
            {
                var statusQuery = from s in _context.OrderStatus select s;

                if(!String.IsNullOrEmpty(SearchString))
                { 
                    statusQuery = statusQuery.Where(s => s.Name.Contains(SearchString));
                }

                NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : sortOrder == "name" ? "name_desc" : "name";
                switch(NameSort)
                {
                    case "name":
                        statusQuery = statusQuery.OrderBy(s => s.Name);
                        break;

                    case "name_desc":
                        statusQuery = statusQuery.OrderByDescending(s => s.Name);
                        break;

                    default:
                        statusQuery = statusQuery.OrderBy(s => s.Name);
                        break;
                }


                OrderStatus = await statusQuery.AsNoTracking().ToListAsync();
            }
        }
    }
}
