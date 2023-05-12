using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Pages.Shopping
{
    public class CartModel : CommerceDemoPageModel
    {
        public CartModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<CartModel> logger)
            : base(context, logger)
        {

        }


        public int OrderId { get; set; }

        [BindProperty]
        public List<ProductVM> Products { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SubTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Tax { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPrice { get; set; }




        public IActionResult OnGet(int customerId)
        {
            if ( _context.Product == null)
            {
                return NotFound();
            }

            var order = GetOrderByCustomer(customerId);
            OrderId = order.Id;
            SubTotal = order.Subtotal;
            Tax = order.Tax;
            TotalPrice = order.TotalPrice;
            Products = new List<ProductVM>();
            if (order.Products != null)
            {
                foreach (var op in order.Products)
                    Products.Add(new ProductVM(op));
            }

            return Page();
        }

        public IActionResult OnPost(int customerId, int productId, int quantity)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            var order = GetOrderByCustomer(customerId);
            if (order.Products == null)
                order.Products = new List<OrderProduct>();

            var product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            order.Products.Add(new OrderProduct { ProductId = productId, Quantity = quantity, Price =  product.Price});

            if (order.OrderHistory == null) 
            {
                order.OrderHistory = new List<OrderHistory>
                {
                    new OrderHistory { OrderStatusId = (int)OrderState.Cart }
                };
            }

            _context.SaveChanges();

            var dict = new RouteValueDictionary
            {
                { "customerId", customerId }
            };
            return RedirectToAction("Index", dict);
        }

        public IActionResult OnPostEdit(int orderId, int orderProductId, int quantity, string action) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.Product == null)
            {
                return NotFound();
            }

            var order = GetOrderById(orderId);

            var prod = order.Products.Where(op => op.Id == orderProductId).FirstOrDefault();
            if (prod != null)
            {
                if (action == "update")
                {
                    if (quantity > 0)
                    {
                        prod.Quantity = quantity;
                    }
                    else
                    {
                        order.Products.Remove(prod);

                    }
                }
                else if (action == "remove")
                {
                    order.Products.Remove(prod);
                }
                
            }
            
            _context.SaveChanges();

            var dict = new RouteValueDictionary();
            dict.Add("customerId", order.CustomerId);
            return RedirectToAction("Index", dict);
        }

        private Order GetOrderByCustomer(int customerId)
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

        private Order GetOrderById(int orderId)
        {
            var order = _context.Order
                .Where(o => o.Id == orderId)
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


        public class ProductVM
        {
            public ProductVM(OrderProduct op)
            {
                Id = op.Id;
                ProductId = op.ProductId;
                Title = op.Product.Title;
                Brand = op.Product.Brand;
                Price = op.Price;
                Quantity = op.Quantity;
            }

            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Title { get; set; } = default!;
            public string Brand { get; set; } = default!;

            
            [DisplayFormat(DataFormatString = "{0:C}")]
            public decimal Price { get; set; }

            [Required]
            [Range(0, int.MaxValue, ErrorMessage = "The Quantity field must be greater than or equal to zero.")]
            public int Quantity { get; set; }

            [DisplayFormat(DataFormatString = "{0:C}")]
            public decimal TotalPrice { 
                get
                {
                    return Quantity * Price;
                }
                    
            }
            
        }
    }



}
