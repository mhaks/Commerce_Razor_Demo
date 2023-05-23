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


        [BindProperty(SupportsGet = true)]
        public int? OrderSearchId { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? CustomerSearchId {  get; set; } = default!;

        public SelectList? OrderStatusSelect { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? OrderStatusFilterId { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                
                var query = from o in _context.Order select o;
                    

                if (OrderSearchId.HasValue && OrderSearchId > 0)
                {
                    query = query.Where(o => o.Id == OrderSearchId);
                }

                if (CustomerSearchId.HasValue && CustomerSearchId > 0)
                {
                    query = query.Where(o => o.Customer.Id == CustomerSearchId);
                }

                if (OrderStatusFilterId.HasValue && OrderStatusFilterId > 0)
                {
                    query = query.Where(o => o.OrderHistory.OrderBy(x => x.OrderDate).Last().OrderStatus.Id == OrderStatusFilterId);
                }

                Orders = await query.AsNoTracking()
                    .Include(o => o.OrderProducts)
                    .ThenInclude(p => p.Product)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.StateLocation)
                    .Include(o => o.OrderHistory)
                    .ThenInclude(o => o.OrderStatus)
                    .OrderBy(o => o.Id)
                    .ToListAsync();

                OrderStatusSelect = new SelectList(await _context.OrderStatus.OrderBy(o => o.Name).ToListAsync(), "Id", "Name");
            }
        }
    }
}
