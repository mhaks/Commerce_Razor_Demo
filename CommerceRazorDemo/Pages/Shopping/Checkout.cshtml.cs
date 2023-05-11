using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class CheckoutModel : CommerceDemoPageModel
    {
        public CheckoutModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<CheckoutModel> logger)
            : base(context, logger)
        {

        }

        public void OnGet(int orderId)
        {
        }

        public async void OnPostAsync(int orderId) 
        { 
            var order = await _context.Order.Where(x => x.Id == orderId).FirstOrDefaultAsync();
        }
    }
}
