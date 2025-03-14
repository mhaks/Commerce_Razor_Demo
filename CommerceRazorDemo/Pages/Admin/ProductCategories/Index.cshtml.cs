using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace CommerceRazorDemo.Pages.ProductCategories
{

    [Authorize(Roles = "ADMIN")]
    public class IndexModel : CommerceDemoPageModel
    {
        public IndexModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        public IList<ProductCategory> ProductCategories { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string TitleSort { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            if (_context == null)
                return NotFound();
 
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

            ProductCategories = await categoryQuery.ToListAsync();
            return Page();

        }
    }
}
