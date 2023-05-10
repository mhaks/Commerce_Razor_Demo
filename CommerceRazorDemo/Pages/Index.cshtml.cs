using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class IndexModel : CommerceDemoPageModel
    {
    
        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<IndexModel> logger)
            : base(context, logger)
        {

        }

        
        public void OnGet()
        {

        }
    }
}