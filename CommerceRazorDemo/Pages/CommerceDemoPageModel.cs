using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CommerceRazorDemo.Pages
{
    public class CommerceDemoPageModel : PageModel
    {
        protected readonly CommerceDemo.Data.CommerceDemoContext _context;
        protected readonly ILogger<CommerceDemoPageModel> _logger;

        public CommerceDemoPageModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<CommerceDemoPageModel> logger)
        {
            _context = context;
            _logger = logger;

        }

        public string? UserId { get; set; } = String.Empty;

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            base.OnPageHandlerExecuting(context);
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            
            ViewData["DbContext"] = _context;
            base.OnPageHandlerExecuted(context);
        }       

    }
}
