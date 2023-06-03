using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CommerceRazorDemo.Pages.Shopping
{
    [Authorize(Roles = "CUSTOMER, ADMIN")]
    public class ProductModel : CommerceDemoPageModel
    {        
        public ProductModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<ProductModel> logger)
            : base(context, logger)
        {

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
