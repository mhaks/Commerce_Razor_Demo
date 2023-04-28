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

        public IActionResult OnPost(int customerId, int productId, int quantity)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            Order = GetCartOrder(customerId);
            if (Order.Products == null)
                Order.Products = new List<OrderProduct>();

            var product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            Order.Products.Add(new OrderProduct { ProductId = productId, Quantity = quantity, Price =  product.Price});

            if (Order.OrderHistory == null) 
            {
                Order.OrderHistory = new List<OrderHistory>();
                Order.OrderHistory.Add(new OrderHistory { OrderStatusId = (int)OrderState.Cart });
            }

            _context.SaveChanges();

            var dict = new RouteValueDictionary();
            dict.Add("customerId", customerId);
            return RedirectToAction("Index", dict);
        }

        public IActionResult OnPostEdit(int customerId, int productId, int quantity) 
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            Order = GetCartOrder(customerId);

            var prod = Order.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (prod != null)
            {
                if (quantity > 0)
                {
                    prod.Quantity = quantity;
                }
                else
                {
                    Order.Products.Remove(prod);  

                }
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        private Order GetCartOrder(int customerId)
        {
            var order = _context.Order
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.Customer)
                .ThenInclude(c => c.StateLocation)
                .Include(c => c.OrderHistory)
                .Include(c => c.Products)
                .ThenInclude(p => p.Product)
                .OrderByDescending(x => x.Id)
                .LastOrDefault();

            if (order == null)
            {
                return new Order();

            }

            if (order.OrderHistory != null) 
            {
                var history = order.OrderHistory.OrderByDescending(x => x.Id).LastOrDefault();
                if (history == null || history.OrderStatusId != (int)OrderState.Cart)
                {
                    return new Order();
                }
            }          
            

            return order;
        }

       
    }

}
