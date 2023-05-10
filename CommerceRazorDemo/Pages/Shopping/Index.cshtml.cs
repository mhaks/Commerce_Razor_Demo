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
    public class IndexModel : CommerceDemoPageModel
    {
        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Category { get; set; }
       

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (Context.Product != null)
            {
                var productsQuery = from p in Context.Product select p;

                if (!string.IsNullOrEmpty(SearchString))
                { 
                    SearchString = SearchString.ToUpper().Trim();
                    productsQuery = productsQuery.Where(p => p.Title.ToUpper().Contains(SearchString) || p.Description.ToUpper().Contains(SearchString) || p.Brand.ToUpper().Contains(SearchString) || p.ProductCategory.Title.ToUpper().Contains(SearchString));

                }
                else if (Category != null)
                {
                    productsQuery = productsQuery.Where(c => c.ProductCategoryId == Category);    
                }

                Products = await productsQuery
                    .AsNoTracking()
                    .Include(c => c.ProductCategory)
                    .ToListAsync();
            }
        }
    }
}
