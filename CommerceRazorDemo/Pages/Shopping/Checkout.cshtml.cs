using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class CheckoutModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<IndexModel> _logger;

        public CheckoutModel(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
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
