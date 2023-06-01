using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class IndexModel : CommerceDemoPageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger, SignInManager<IdentityUser> signInManager) : base(context, logger)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGetAsync()
        {
            if (User.IsInRole("Customer"))
            {
                return RedirectToPage("/Shopping/Index");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToPage("/Admin/Index");
            }

            return RedirectToPage("/Shopping/Index");
        }

        public async Task<IActionResult> OnPost(int userId) 
        { 
            if(_context == null)
                return NotFound();

            if (userId == 0)
            {
                // sign into admin
            }
            else if (userId > 0) {
                var user = _context.Customer.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    // Sign out the current user
                    await _signInManager.SignOutAsync();

                    // Sign in the new user
                    var iuser = new IdentityUser(user.UserName);
                    iuser.Id = user.Id.ToString();

                    await _signInManager.SignInAsync(iuser, isPersistent: false);
                }
            }
            

            return RedirectToAction("Index");
        }
    }
}
