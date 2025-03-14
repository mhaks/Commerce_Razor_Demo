namespace CommerceRazorDemo.Pages
{
    public class AccessDeniedModel : CommerceDemoPageModel
    {
        public AccessDeniedModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public void OnGet()
        {
        }
    }
}
