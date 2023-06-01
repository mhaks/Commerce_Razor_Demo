using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages.Admin
{
    public class IndexModel : CommerceDemoPageModel
    {
        public IndexModel(CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public void OnGet()
        {
        }
    }
}
