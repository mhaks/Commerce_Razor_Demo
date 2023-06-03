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
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CommerceRazorDemo.Pages.ProductCategories
{
    [Authorize(Roles = "ADMIN")]
    public class EditModel : CommerceDemoPageModel
    {
                
        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<EditModel> logger)
            : base(context, logger)
        {

        }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = String.Empty;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (id.HasValue && id != 0)
            {
                var productcategory = await _context.ProductCategory.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (productcategory == null)
                {
                    return NotFound();
                }

                CategoryId = productcategory.Id;
                Title = productcategory.Title;
            }
            else
            {
                CategoryId = 0;
                Title = String.Empty;
            }
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CategoryId != 0)
            {
                var productcategory = await _context.ProductCategory.FirstOrDefaultAsync(m => m.Id == CategoryId);
                if (productcategory == null)
                {
                    return NotFound();
                }
                productcategory.Title = Title;               
            }
            else
            {
                _context.ProductCategory.Add(new ProductCategory { Title = Title });
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(CategoryId))
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

        private bool ProductCategoryExists(int id)
        {
          return (_context.ProductCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
