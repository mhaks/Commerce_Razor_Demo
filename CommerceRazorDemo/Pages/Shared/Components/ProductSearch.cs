using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductSearchViewComponent : ViewComponent
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext Context;
        private readonly ILogger<IndexModel> _logger;

        public ProductSearchViewComponent(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            Context = context;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
