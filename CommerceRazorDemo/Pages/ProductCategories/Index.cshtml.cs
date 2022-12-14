using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.Data.SqlClient;

namespace CommerceRazorDemo.Pages.ProductCategories
{
    public class IndexModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        public IList<ProductCategory> ProductCategory { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string TitleSort { get; set; } = string.Empty;

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.ProductCategory != null)
            {
                var categoryQuery = from c in _context.ProductCategory
                                    select c;

                if (!String.IsNullOrEmpty(SearchString))
                    categoryQuery = categoryQuery.Where(c => c.Title.Contains(SearchString));

                TitleSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                switch(sortOrder)
                {
                    case "name_desc":
                        categoryQuery = categoryQuery.OrderByDescending(c => c.Title);
                        break;
                    default:
                        categoryQuery = categoryQuery.OrderBy(c => c.Title);
                        break;
                }

                ProductCategory = await categoryQuery.AsNoTracking().ToListAsync();
            }
        }
    }
}
