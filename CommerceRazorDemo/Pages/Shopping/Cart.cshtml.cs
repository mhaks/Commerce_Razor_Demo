using CommerceRazorDemo.Data;

using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommerceRazorDemo.Pages.Shopping
{
    [Authorize(Roles = "CUSTOMER")]
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


        public async Task<IActionResult> OnGetAsync()
        {
            if (_context == null)
            {
                return NotFound();
            }

            var query = _context.Order.Where(c => c.UserId == UserId).AsNoTracking();
            var order = await GetOrder(query);

            OrderId = order.Id;
            SubTotal = order.Subtotal;
            Products = new List<ProductVM>();
            if (order.OrderProducts != null)
            {
                foreach (var op in order.OrderProducts)
                    Products.Add(new ProductVM(op));
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int productId, int quantity)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            var product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            var query = _context.Order.Where(c => c.UserId == UserId);
            var order = await GetOrder(query);

            order.OrderProducts ??= new List<OrderProduct>();
            var oproduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);
            if (oproduct != null)
                oproduct.Quantity += quantity;
            else
                order.OrderProducts.Add(new OrderProduct { Order = order, ProductId = productId, Quantity = quantity, Price =  product.Price});

            
            order.OrderHistory ??= new List<OrderHistory>
            {
                new OrderHistory { Order = order, OrderDate = DateTime.UtcNow, OrderStatusId = (int)OrderState.Cart }
            };

            if (order.Id == 0 && !String.IsNullOrEmpty(UserId))
            {
                order.UserId = UserId;
                _context.Order.Add(order);
            }
            
            await _context.SaveChangesAsync();

           
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> OnPostEditAsync(int orderId, int orderProductId, int quantity, string action) 
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var query = _context.Order.Where(o => o.Id == orderId);
            var order = await GetOrder(query);

            var prod = order.OrderProducts.Where(op => op.Id == orderProductId).FirstOrDefault();
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
                        order.OrderProducts.Remove(prod);

                    }
                }
                else if (action == "remove")
                {
                    order.OrderProducts.Remove(prod);
                }
                
            }

            // no products, remove empty order, no
            if (!order.OrderProducts.Any()) 
            { 
                _context.Order.Remove(order); 
            }

            await _context.SaveChangesAsync();

          
            return RedirectToAction("Index");
        }
              


        private async Task<Order> GetOrder(IQueryable<Order> query)
        {
            var order = await query
                .Include(c => c.User)
                .ThenInclude(c => c.StateLocation)
                .Include(c => c.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(c => c.OrderHistory)
                .ThenInclude(c => c.OrderStatus)
                .OrderBy(x => x.Id)
                .LastOrDefaultAsync();

            if (order == null || order.OrderHistory == null || !order.OrderHistory.Any())
            {
                return new Order();

            }

            var history = order.OrderHistory.OrderBy(x => x.OrderDate).LastOrDefault();
            if (history == null || history.OrderStatusId != (int)OrderState.Cart)
            {
                return new Order();
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
