using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class CartStatusViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext Context;
        private readonly ILogger<IndexModel> _logger;

        public CartStatusViewComponent(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            Context = context;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // TODO 
            var orders = await Context.Order
                .Where(x => x.CustomerId == 0)
                .AsNoTracking()
                .ToListAsync();

            return View(false);
        }
    }
}
