﻿using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class CartStatusViewComponent : ViewComponent
    {
      
        public CartStatusViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var context = ViewData["DbContext"] as CommerceDemo.Data.CommerceDemoContext;

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int itemCount = 0;


            if (context != null)
            {
                var order = await context.Order
                   .AsNoTracking()
                   .Where(o => o.UserId == userId)
                   .Include(c => c.OrderHistory)
                   .Include(c => c.OrderProducts)
                   .OrderBy(x => x.Id)
                   .LastOrDefaultAsync();

                if (order != null && order.OrderHistory != null)
                {
                    var history = order.OrderHistory.OrderBy(x => x.OrderDate).LastOrDefault();
                    if (history != null && history.OrderStatusId == (int)OrderState.Cart)
                    {
                        itemCount = order.OrderProducts.Count;
                    }
                }
            }         


            return View(itemCount);
        }
    }
}
