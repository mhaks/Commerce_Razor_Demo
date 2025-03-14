using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommerceRazorDemo.Pages.Admin
{
    [Authorize(Roles = "ADMIN")]
    public class IndexModel : CommerceDemoPageModel
    {
        public IndexModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<CommerceDemoPageModel> logger) : base(context, logger)
        {
        }

        public int SalesOrderCount { get; set; }
        public int SalesProductCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SalesDollarRevenue { get; set; }

        public int OrderCartCount { get; set; }
        public int OrderProcessingCount { get; set; }
        public int OrderShippedCount { get; set; }
        public int OrderDeliveredCount { get; set; }
        public int OrderReturnCount { get; set; }

        public int InventoryActiveCount { get; set; }
        public int InventoryStockGoodCount { get; set;}
        public int InventoryStockLowCount { get; set; }
        public int InventoryStockOutCount { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if(_context == null)
                return NotFound();

            // orders made within last 7 days, by processing state in history
            var sevenBackDate = DateTime.Now.AddDays(-7);
            var lookbackDate = new DateTime(sevenBackDate.Year, sevenBackDate.Month, sevenBackDate.Day, 0, 0, 0);

            var sales = await _context.Order
                            .Where(o => o.OrderHistory.Any(h => h.OrderStatusId == (int)OrderState.Processing && h.OrderDate >= lookbackDate))
                            .Include(o => o.OrderProducts)
                            .AsNoTracking()
                            .ToListAsync();

            SalesOrderCount = sales.Count();
            SalesProductCount = sales.Sum(o => o.OrderProducts.Count());
            SalesDollarRevenue = sales.Sum(o => o.Subtotal);


            var carts = await _context.Order
                .Where(o => o.OrderHistory.OrderByDescending(h => h.OrderDate).First().OrderStatusId == (int)OrderState.Cart)
                .AsNoTracking()
                .ToListAsync();

            OrderCartCount = carts.Count();


            var ordered = await _context.Order
                .Where(o => o.OrderHistory.OrderByDescending(h => h.OrderDate).First().OrderStatusId == (int)OrderState.Processing)
                .AsNoTracking()
                .ToListAsync();

            OrderProcessingCount = ordered.Count();

            var shipped = await _context.Order
                .Where(o => o.OrderHistory.OrderByDescending(h => h.OrderDate).First().OrderStatusId == (int)OrderState.Shipped)
                .AsNoTracking()
                .ToListAsync();

            OrderShippedCount = shipped.Count();

            var delivered = await _context.Order
                            .Where(o => o.OrderHistory.Any(h => h.OrderStatusId == (int)OrderState.Delivered && h.OrderDate >= lookbackDate))
                            .Include(o => o.OrderProducts)
                            .AsNoTracking()
                            .ToListAsync();

            OrderDeliveredCount = delivered.Count();

            var returned = await _context.Order
                            .Where(o => o.OrderHistory.Any(h => h.OrderStatusId == (int)OrderState.Returned && h.OrderDate >= lookbackDate))
                            .Include(o => o.OrderProducts)
                            .AsNoTracking()
                            .ToListAsync();

            OrderReturnCount = returned.Count();

            var products = await _context.Product.Where(x => x.IsActive).AsNoTracking().ToListAsync();


            InventoryActiveCount = products.Count();
            InventoryStockGoodCount = products.Where(x => x.AvailableQty >= 10).Count();
            InventoryStockLowCount = products.Where(x => x.AvailableQty < 10).Count();
            InventoryStockOutCount = products.Where(x => x.AvailableQty == 0).Count();

            return Page();
        }
    }
}
