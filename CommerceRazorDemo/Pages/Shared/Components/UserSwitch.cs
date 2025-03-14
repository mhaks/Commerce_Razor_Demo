using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class UserSwitchViewComponent : ViewComponent
    {
        public UserSwitchViewComponent()
        {

        }



        public async Task<IViewComponentResult> InvokeAsync()
        {
            var context = ViewData["DbContext"] as CommerceDemo.Data.CommerceDemoContext;

            SelectList users;
            if (context != null)
            {
                string? username = HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : null;
                users = new SelectList(await context.Users.OrderBy(c => c.UserName).ToListAsync(), "UserName", "UserName", username);
                
            }
            else
            {
                users = new SelectList(Enumerable.Empty<SelectListItem>(), "UserName", "UserName");
            }



            return View(users);
        }


     
    }
}
