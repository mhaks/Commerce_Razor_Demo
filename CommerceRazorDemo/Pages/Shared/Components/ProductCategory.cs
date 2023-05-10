using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext Context;
        private readonly ILogger<IndexModel> _logger;

        public ProductCategoryViewComponent(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            Context = context;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await Context.ProductCategory
                .OrderBy(x => x.Title)
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }
    }
}
