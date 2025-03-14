using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommerceRazorDemo.Pages.Shopping
{
    [Authorize(Roles = "CUSTOMER")]
    public class CheckoutModel : CommerceDemoPageModel
    {
        public CheckoutModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<CheckoutModel> logger)
            : base(context, logger)
        {

        }

        public List<SelectListItem> Expirations { get; set; } = default!;


        public Order Order { get; set; } = default!;

        [Required]
        [BindProperty]
        [MinLength(2)]
        [Display(Name = "Cardholder Name")]
        public string CardName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [MinLength(16)]
        [MaxLength(16)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string CardExpiration { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        [Display(Name = "CCV")]
        public string CardCCV { get; set; } = string.Empty;


        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            if (_context == null)
                return NotFound();

            LoadExpirations();

            var order = await _context.Order
                .Where(o => o.Id == orderId && o.UserId == UserId)
                .Include(c => c.User)
                .ThenInclude(s => s.StateLocation)
                .Include(p => p.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(h => h.OrderHistory)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null) 
                return NotFound();


            if (!order.OrderProducts.Any() || !order.OrderHistory.Any() || order.OrderHistory.OrderByDescending(x => x.Id).Last().OrderStatusId != (int)OrderState.Cart)
                return RedirectToPage("Cart", new { customerId = order.UserId });

            Order = order;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId) 
        {
            if (!ModelState.IsValid)
            {
                LoadExpirations();
                return Page();
            }

            var order = await _context.Order
                .Where(o => o.Id == orderId && o.UserId == UserId)
                .Include(p => p.OrderProducts)
                .ThenInclude(p => p.Product)
                .Include(h => h.OrderHistory)                
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            var processing = new OrderHistory { OrderId = orderId, OrderDate = DateTime.UtcNow, OrderStatusId = (int)OrderState.Processing };
            order.OrderHistory.Add(processing);

            foreach(var item in order.OrderProducts)
            {
                var product = _context.Product.Where(p => p.Id == item.ProductId).FirstOrDefault();
                if (product == null) continue;
                product.AvailableQty = product.AvailableQty - item.Quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Order", new { orderId = orderId });
            
        }


        private void LoadExpirations()
        {
            Expirations = new List<SelectListItem>();
            for (var i = 0; i < 48; i++)
            {
                var exp = DateTime.Now.AddMonths(i);
                var val = $"{exp.Month}/{exp.Year}";
                Expirations.Add(new SelectListItem(val, val));
            }
        }
    }
}
