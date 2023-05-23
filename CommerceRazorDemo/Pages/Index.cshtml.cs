using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages
{
    public class IndexModel : CommerceDemoPageModel
    {
    
        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        public List<Models.Product> TopProducts { get; set; } = default!;
        
        public async Task OnGet()
        {


            var products  = await _context.Product
                .OrderByDescending(a => _context.OrderProduct.Count(b => b.ProductId == a.Id))
                .Take(4)
                .ToListAsync();

            TopProducts = products;

        }
    }
}