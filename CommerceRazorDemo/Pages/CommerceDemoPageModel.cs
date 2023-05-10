using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages
{
    public class CommerceDemoPageModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<CommerceDemoPageModel> _logger;

        public CommerceDemoPageModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<CommerceDemoPageModel> logger)
        {
            _context = context;
            _logger = logger;
            IsAdmin = false;
        }

        public CommerceRazorDemo.Data.CommerceRazorDemoContext Context { get => _context; }
        public ILogger<CommerceDemoPageModel> Logger { get => _logger; }

        public bool IsAdmin { get; set; }
    }
}
