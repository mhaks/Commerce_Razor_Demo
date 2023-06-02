using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommerceRazorDemo.Pages.Orders
{
    public class IndexModel : CommerceDemoPageModel
    {
        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        public IList<Order> Orders { get;set; } = default!;

        public SelectList? OrderStatusSelect { get; set; } = default!;

        public SelectList ProcessStates { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? OrderSearchId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CustomerSearchId {  get; set; } = default!;

        

        [BindProperty(SupportsGet = true)]
        public int? OrderStatusFilterId { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context == null)
            {
                return NotFound();
            }

                
            var query = from o in _context.Order select o;
                    

            if (OrderSearchId.HasValue && OrderSearchId > 0)
            {
                query = query.Where(o => o.Id == OrderSearchId);
            }

            if (!String.IsNullOrEmpty(CustomerSearchId))
            {
                query = query.Where(o => o.User.UserName == CustomerSearchId);
            }

            if (OrderStatusFilterId.HasValue && OrderStatusFilterId > 0)
            {
                query = query.Where(o => o.OrderHistory.OrderBy(x => x.OrderDate).Last().OrderStatus.Id == OrderStatusFilterId);
            }

            Orders = await query.AsNoTracking()
                .Include(o => o.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(o => o.User)
                .ThenInclude(c => c.StateLocation)
                .Include(o => o.OrderHistory)
                .ThenInclude(o => o.OrderStatus)
                .OrderBy(o => o.Id)
                .ToListAsync();

            OrderStatusSelect = new SelectList(await _context.OrderStatus.OrderBy(o => o.Name).ToListAsync(), "Id", "Name", OrderStatusFilterId.HasValue ? OrderStatusFilterId : 0);

            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Ship", Value = "3" },
                new SelectListItem { Text = "Deliver", Value = "4" },
                new SelectListItem { Text = "Return", Value = "5" },
            };

            ProcessStates = new SelectList(items, "Value", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId, int orderStateId)
        {
            if (_context == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Where(x => x.Id == orderId)
                .Include(x => x.OrderHistory)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            order.OrderHistory.Add(new OrderHistory() { OrderId = orderId, OrderStatusId = orderStateId, OrderDate = DateTime.UtcNow });

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
