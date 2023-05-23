using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class OrderModel : CommerceDemoPageModel
    {
        public OrderModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<OrderModel> logger)
            : base(context, logger)
        {

        }

        public int CustomerId { get; set; }
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            if (_context == null)
                return NotFound();


            var order = await _context.Order
                            .Where(o => o.Id == orderId)
                            .Include(c => c.Customer)
                            .ThenInclude(s => s.StateLocation)
                            .Include(p => p.OrderProducts)
                            .ThenInclude(p => p.Product)
                            .Include(h => h.OrderHistory)
                            .ThenInclude(s => s.OrderStatus)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            // don't need to show initial cart
            order.OrderHistory.Remove(order.OrderHistory.First(h => h.OrderStatusId == (int)OrderState.Cart));


            Order = order;
            CustomerId = order.CustomerId ?? 0;
            return Page();
        }
    }
}
