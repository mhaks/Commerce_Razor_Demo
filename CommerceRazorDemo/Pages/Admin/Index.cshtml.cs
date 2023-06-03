using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace CommerceRazorDemo.Pages.Admin
{
    [Authorize(Roles = "ADMIN")]
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
