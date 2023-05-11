using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductSearchViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<ProductSearchViewComponent> _logger;

        public ProductSearchViewComponent(CommerceRazorDemoContext context, ILogger<ProductSearchViewComponent> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.ProductCategory
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ToListAsync();

            return View(categories);
        }
    }
}
