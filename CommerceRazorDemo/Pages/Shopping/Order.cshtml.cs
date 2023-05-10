using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class OrderModel : CommerceDemoPageModel
    {
        public OrderModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<OrderModel> logger)
            : base(context, logger)
        {

        }

        public void OnGet()
        {
        }
    }
}
