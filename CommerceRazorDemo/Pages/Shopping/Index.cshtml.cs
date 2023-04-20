using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.IdentityModel.Tokens;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class IndexModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

         [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
       

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                var productsQuery = from p in _context.Product select p;

                if (!string.IsNullOrEmpty(SearchString))
                { 
                    SearchString = SearchString.ToUpper().Trim();
                    productsQuery = productsQuery.Where(p => p.Title.ToUpper().Contains(SearchString) || p.Description.ToUpper().Contains(SearchString) || p.Brand.ToUpper().Contains(SearchString) || p.ProductCategory.Title.ToUpper().Contains(SearchString));

                    // wonky search for singular of plural term
                    /*
                    if (SearchString.EndsWith('S')) 
                    {
                        var singular = SearchString.Substring(0, SearchString.Length - 1);
                        productsQuery = productsQuery.Where(p => p.Title.ToUpper().Contains(singular) || p.Description.ToUpper().Contains(singular) || p.Brand.ToUpper().Contains(SearchString) || p.ProductCategory.Title.ToUpper().Contains(singular));
                    }
                    */
                }

                Products = await productsQuery
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
