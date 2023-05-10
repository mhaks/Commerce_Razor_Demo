using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Pages.Orders
{
    public class DetailsModel : CommerceDemoPageModel
    {
        public DetailsModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<DetailsModel> logger)
            : base(context, logger)
        {

        }

        public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || Context.Order == null)
            {
                return NotFound();
            }

            var order = await Context.Order
                .AsNoTracking()
                .Include(x => x.Customer)
                .ThenInclude(x => x.StateLocation)
                .Include(x => x.OrderHistory)
                .ThenInclude(x => x.OrderStatus)
                .Include(x => x.Products)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }
    }
}
