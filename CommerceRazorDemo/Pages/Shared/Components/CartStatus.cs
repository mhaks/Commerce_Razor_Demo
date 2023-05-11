using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class CartStatusViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<CartStatusViewComponent> _logger;

        public CartStatusViewComponent(CommerceRazorDemoContext context, ILogger<CartStatusViewComponent> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customerId = 1;
            int itemCount = 0;

            var order = await _context.Order
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .Include(c => c.OrderHistory)
                .Include(c => c.Products)
                .OrderByDescending(x => x.Id)
                .LastOrDefaultAsync();

            if (order != null && order.OrderHistory != null)
            {
                var history = order.OrderHistory.OrderByDescending(x => x.Id).LastOrDefault();
                if (history != null && history.OrderStatusId == (int)OrderState.Cart)
                {
                    itemCount = order.Products.Count;
                }
            }
                      

            return View(itemCount);
        }
    }
}
