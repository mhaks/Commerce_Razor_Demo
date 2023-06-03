using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class AccessDeniedModel : CommerceDemoPageModel
    {
        public AccessDeniedModel(CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public void OnGet()
        {
        }
    }
}
