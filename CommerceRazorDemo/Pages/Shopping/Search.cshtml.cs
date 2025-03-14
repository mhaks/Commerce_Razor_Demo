using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class SearchModel : CommerceDemoPageModel
    {
        public SearchModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<SearchModel> logger)
            : base(context, logger)
        {

        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }
       

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context != null && _context.Product != null)
            {
                var productsQuery = from p in _context.Product where p.IsActive select p;

                if (!string.IsNullOrEmpty(SearchString))
                { 
                    var search = SearchString.ToUpper().Trim();
                    
                    productsQuery = productsQuery.Where(p => 
                        p.Title.ToUpper().Contains(search) || 
                        p.Description.ToUpper().Contains(search) || 
                        p.Brand.ToUpper().Contains(search) || 
                        p.ProductCategory.Title.ToUpper().Contains(search));

                }
                
                if (CategoryId != null)
                {
                    productsQuery = productsQuery.Where(c => c.ProductCategoryId == CategoryId);
                    CategoryName = _context.ProductCategory.FirstOrDefault(x => x.Id == CategoryId)?.Title ?? "";
                }

                Products = await productsQuery
                    .AsNoTracking()
                    .Include(c => c.ProductCategory)
                    .Take(20)
                    .ToListAsync();
            }
        }
    }
}
