using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages.Errors
{
    public class _404Model : CommerceDemoPageModel
    {
        public _404Model(CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public void OnGet()
        {
            _logger.LogWarning("404 Page Not Found");
        }
    }
}
