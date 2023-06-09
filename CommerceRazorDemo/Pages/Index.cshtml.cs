using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class IndexModel : CommerceDemoPageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : base(context, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                string defaultUsername = "jerry";

                
                // Find the default user by username
                var defaultUser = await _userManager.FindByNameAsync(defaultUsername);

                if (defaultUser != null)
                {
                    // Sign in the default user
                    await _signInManager.SignInAsync(defaultUser, isPersistent: false);
                    _logger.LogInformation("Login as default user: {defaultUser}", defaultUser);
                }                
            }

            if (User.IsInRole("CUSTOMER"))
            {
                return RedirectToPage("/Shopping/Index");
            }
            else if (User.IsInRole("ADMIN"))
            {
                return RedirectToPage("/Admin/Index");
            }

            return RedirectToPage("/Shopping/Index");
        }

        public async Task<IActionResult> OnPost(string name) 
        { 
            if(_context == null)
                return NotFound();

            if (User.Identity == null || User.Identity.Name != name)
            {
                _logger.LogInformation("Logging in as user: {name}", name);
                var user = await _userManager.FindByNameAsync(name);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("Logged in as user: {name}", name);
                }
            }
           

            return RedirectToAction("Index");
        }
    }
}
