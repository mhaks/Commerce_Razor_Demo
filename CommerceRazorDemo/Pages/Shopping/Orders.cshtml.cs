using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class OrdersModel : CommerceDemoPageModel
    {
        public OrdersModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<OrdersModel> logger)
            : base(context, logger)
        {

        }

        public void OnGet()
        {
        }
    }
}
