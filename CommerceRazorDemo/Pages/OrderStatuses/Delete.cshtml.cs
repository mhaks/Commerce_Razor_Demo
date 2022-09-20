using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Pages.OrderStatuses
{
    public class DeleteModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public DeleteModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderStatus OrderStatus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderStatus == null)
            {
                return NotFound();
            }

            var orderstatus = await _context.OrderStatus.FirstOrDefaultAsync(m => m.Id == id);

            if (orderstatus == null)
            {
                return NotFound();
            }
            else 
            {
                OrderStatus = orderstatus;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OrderStatus == null)
            {
                return NotFound();
            }
            var orderstatus = await _context.OrderStatus.FindAsync(id);

            if (orderstatus != null)
            {
                OrderStatus = orderstatus;
                _context.OrderStatus.Remove(OrderStatus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
