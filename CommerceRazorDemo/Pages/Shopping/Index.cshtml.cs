using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    [Authorize(Roles = "CUSTOMER")]
    public class IndexModel : CommerceDemoPageModel
    {

        public IndexModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<SearchModel> logger)
            : base(context, logger)
        {

        }

        public List<Product> TopProducts { get; set; } = default!;


        public async Task OnGet()
        {

            var products = await _context.Product
                .OrderByDescending(a => _context.OrderProduct.Count(b => b.ProductId == a.Id))
                .Take(4)
                .ToListAsync();

            TopProducts = products;

        }
    }
}