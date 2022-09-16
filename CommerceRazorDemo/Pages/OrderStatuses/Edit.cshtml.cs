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

namespace CommerceRazorDemo.Pages.OrderStatuses
{
    public class EditModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderStatus OrderStatus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context.OrderStatus == null)
            {
                return NotFound();
            }

            if (id.HasValue && id != 0)
            {
                var orderstatus = await _context.OrderStatus.FirstOrDefaultAsync(m => m.Id == id);
                if (orderstatus == null)
                {
                    return NotFound();
                }
                OrderStatus = orderstatus;
            }
            else
            {
                OrderStatus = new OrderStatus { Id = 0 };
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

            if (OrderStatus.Id != 0)
            {
                _context.Attach(OrderStatus).State = EntityState.Modified;
            }
            else
            {
                _context.Attach(OrderStatus).State = EntityState.Added;
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(OrderStatus.Id))
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
}
