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

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            if (_context == null)
                return NotFound();

            var order = await _context.Order
                .Where(o => o.Id == orderId)
                .Include(c => c.Customer)
                .ThenInclude(s => s.StateLocation)
                .Include(p => p.Products)
                .ThenInclude(p => p.Product)
                .Include(h => h.OrderHistory)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            Order = order;
            return Page();
        }
    }
}
