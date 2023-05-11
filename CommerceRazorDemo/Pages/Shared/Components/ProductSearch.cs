using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductSearchViewComponent : ViewComponent
    {

        public ProductSearchViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var context = ViewData["DbContext"] as CommerceRazorDemo.Data.CommerceRazorDemoContext;

            var categories = await context.ProductCategory
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ToListAsync();

            //var categories = new List<Models.ProductCategory>();

            return View(categories);
        }
    }
}
