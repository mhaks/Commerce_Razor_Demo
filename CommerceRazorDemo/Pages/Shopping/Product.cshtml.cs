using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class ProductModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<IndexModel> _logger;

        public ProductModel(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .AsNoTracking()
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }
    }
}
