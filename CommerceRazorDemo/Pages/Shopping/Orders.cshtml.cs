using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class OrdersModel : CommerceDemoPageModel
    {
        public OrdersModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<OrdersModel> logger)
            : base(context, logger)
        {

        }

        public List<Order> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGet(int customerId)
        {
            if (_context == null)
                return NotFound();

            var orders = await _context.Order
                            .Where(o => o.CustomerId == customerId)
                            .Include(p => p.Products)
                            .Include(h => h.OrderHistory) 
                            .ThenInclude(s => s.OrderStatus)
                            .Include(c => c.Customer)
                            .ThenInclude(s => s.StateLocation)
                            .OrderBy(o => o.OrderHistory.First().OrderDate)
                            .AsNoTracking()
                            .ToListAsync();

            foreach (var order in orders)
            {
                // don't need to show order in cart
                var history = order.OrderHistory.OrderBy(x => x.OrderDate).LastOrDefault();
                if (history == null || history.OrderStatusId == (int)OrderState.Cart)
                {
                    orders.Remove(order);
                    break;
                }
            }

            Orders = orders;
            return Page();
        }
    }
}
