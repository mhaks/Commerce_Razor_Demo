using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class CartModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<IndexModel> _logger;

        public CartModel(CommerceRazorDemoContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }


        public Order? Order { get; set; }

        public IActionResult OnGet(int customerId)
        {
            if ( _context.Product == null)
            {
                return NotFound();
            }

            Order = GetCartOrder(customerId);

            return Page();
        }

        public IActionResult OnPostAddProduct(int customerId, int productId, int quantity)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            Order = GetCartOrder(customerId);
            if (Order.Products.Any())
            {
                Order.Products.Add(new OrderProduct { ProductId = productId, Quantity = quantity });
            }

            if (Order.OrderHistory.Any()) 
            {
                Order.OrderHistory.Add(new OrderHistory { OrderStatusId = (int)OrderState.Cart });
            }

            _context.SaveChanges();

            return new RedirectToPageResult(nameof(OnGet));
        }

        public IActionResult OnPostEditProduct(int customerId, int productId, int quantity) 
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            // has order
            // has history, last = cart
            // add product to existing or new order

            return new RedirectToPageResult(nameof(OnGet));
        }


        private Order GetCartOrder(int customerId)
        {
            var order = _context.Order.Where(c => c.CustomerId == customerId).OrderByDescending(x => x.Id).LastOrDefault();
            if (order == null)
            {
                return new Order();

            }

            var history = order.OrderHistory.LastOrDefault();
            if (history == null || history.OrderStatusId != (int)OrderState.Cart)
            {
                return new Order();
            }

            return order;
        }

       
    }

}
