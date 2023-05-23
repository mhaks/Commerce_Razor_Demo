using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class CommerceDemoPageModel : PageModel
    {
        protected readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        protected readonly ILogger<CommerceDemoPageModel> _logger;

        public CommerceDemoPageModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger)
        {
            _context = context;
            _logger = logger;

            

            // TODO is user a customer or admin role
            IsAdmin = false;
        }

        public bool IsAdmin { get; set; }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            ViewData["DbContext"] = _context;
            base.OnPageHandlerExecuted(context);
        }       

    }
}
