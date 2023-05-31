using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class UserSwitchViewComponent : ViewComponent
    {
        public UserSwitchViewComponent()
        {

        }



        public async Task<IViewComponentResult> InvokeAsync()
        {
            var context = ViewData["DbContext"] as CommerceRazorDemo.Data.CommerceRazorDemoContext;

            SelectList users = null;
            if (context != null)
            {
                users = new SelectList(await context.Customer.OrderBy(c => c.LastName).ToListAsync(), "Id", "FullName");
            }
            
            
            
            return View(users);
        }


     
    }
}
