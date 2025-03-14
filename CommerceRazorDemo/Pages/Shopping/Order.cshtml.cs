using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CommerceRazorDemo.Pages.Shopping
{
    [Authorize(Roles = "CUSTOMER")]
    public class OrderModel : CommerceDemoPageModel
    {
        public OrderModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<OrderModel> logger)
            : base(context, logger)
        {

        }

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            if (_context == null)
                return NotFound();


            var order = await _context.Order
                            .Where(o => o.Id == orderId && o.UserId == UserId)
                            .Include(c => c.User)
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

            return Page();
        }
    }
}
