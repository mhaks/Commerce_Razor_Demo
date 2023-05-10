﻿using System;
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

namespace CommerceRazorDemo.Pages.ProductCategories
{
    public class EditModel : CommerceDemoPageModel
    {
        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<EditModel> logger)
            : base(context, logger)
        {

        }

        [BindProperty]
        public ProductCategoryVM ProductCategoryVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (Context.ProductCategory == null)
            {
                return NotFound();
            }

            if (id.HasValue && id != 0)
            {
                var productcategory = await Context.ProductCategory.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (productcategory == null)
                {
                    return NotFound();
                }
                ProductCategoryVM = new ProductCategoryVM { Id = productcategory.Id, Title = productcategory.Title };
            }
            else
            {
                ProductCategoryVM = new ProductCategoryVM { Id = 0 };
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

            if (ProductCategoryVM.Id != 0)
            {
                var productcategory = await Context.ProductCategory.FirstOrDefaultAsync(m => m.Id == ProductCategoryVM.Id);
                if (productcategory == null)
                {
                    return NotFound();
                }
                productcategory.Title = ProductCategoryVM.Title;               
            }
            else
            {
                Context.ProductCategory.Add(new ProductCategory { Title = ProductCategoryVM.Title });
            }
            

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(ProductCategoryVM.Id))
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
          return (Context.ProductCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

    public class ProductCategoryVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = String.Empty;
    }
}
