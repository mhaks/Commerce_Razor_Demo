namespace CommerceRazorDemo.Pages.Errors
{
    public class _404Model : CommerceDemoPageModel
    {
        public _404Model(CommerceDemo.Data.CommerceDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public void OnGet()
        {
            _logger.LogWarning("404 Page Not Found");
        }
    }
}
