using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<IndexModel> _logger;

        public ProductCategoryViewComponent(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.ProductCategory
                .OrderBy(x => x.Title)
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }
    }
}
