using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Pages.OrderStatuses
{
    public class EditModel : CommerceDemoPageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;
        private readonly ILogger<CommerceDemoPageModel> _logger;

        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<EditModel> logger)
             : base(context, logger)
        {

        } 

        [BindProperty]
        public OrderStatusVM OrderStatusVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context.OrderStatus == null)
            {
                return NotFound();
            }

            if (id.HasValue && id != 0)
            {
                var orderstatus = await _context.OrderStatus.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (orderstatus == null)
                {
                    return NotFound();
                }
                OrderStatusVM = new OrderStatusVM { Id = orderstatus.Id, Name = orderstatus.Name };
            }
            else
            {
                OrderStatusVM = new OrderStatusVM { Id = 0 };
            }
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (OrderStatusVM.Id != 0)
            {
                var orderStatus = await _context.OrderStatus.FirstOrDefaultAsync(x => x.Id == OrderStatusVM.Id);
                if (orderStatus == null)
                {
                    return NotFound();
                }
                orderStatus.Name = OrderStatusVM.Name;                               
            }
            else
            {
                var orderStatus = new OrderStatus { Name = OrderStatusVM.Name };
                _context.OrderStatus.Add(orderStatus);                
            }
                        

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (OrderStatusVM.Id != 0 && !OrderStatusExists(OrderStatusVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderStatusExists(int id)
        {
          return _context.OrderStatus.Any(e => e.Id == id);
        }
    }

    public class OrderStatusVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = String.Empty;
    }
}
