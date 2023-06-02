using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class OrdersModel : CommerceDemoPageModel
    {
        public OrdersModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<OrdersModel> logger)
            : base(context, logger)
        {

        }

        public List<Order> Orders { get; set; } = default!;
        public SelectList? StatusList { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? StatusFilterId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (_context == null)
                return NotFound();

            StatusList = new SelectList(await _context.OrderStatus.Where(x => x.Id > 1).OrderBy(x => x.Name).ToListAsync(), "Id", "Name");

            var orders = await _context.Order
                            .Where(o => o.UserId == UserId)
                            .Include(p => p.OrderProducts)
                            .Include(h => h.OrderHistory) 
                            .ThenInclude(s => s.OrderStatus)
                            .Include(c => c.User)
                            .ThenInclude(s => s.StateLocation)
                            .OrderByDescending(o => o.OrderHistory.First().OrderDate)
                            .AsNoTracking()
                            .ToListAsync();

            var removes = new List<Order>();
            foreach (var order in orders)
            {
                // don't need to show order in cart
                var history = order.OrderHistory.OrderBy(x => x.OrderDate).LastOrDefault();
                if (history == null || history.OrderStatusId == (int)OrderState.Cart)
                {
                    removes.Add(order);
                    break;
                }

                if (StatusFilterId.HasValue && history.OrderStatusId != StatusFilterId)
                    removes.Add(order);
            }

            foreach (var item in removes)
                orders.Remove(item);

            Orders = orders;
            return Page();
        }
    }
}
