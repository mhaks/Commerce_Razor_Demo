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
using System.Xml.Linq;

namespace CommerceRazorDemo.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductVM ProductVM { get; set; } = default!;

        public List<string> Brands { get; set; } = default!;        

        public SelectList? Categories { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }

            if (id.HasValue && id.Value != 0)
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                ProductVM = new ProductVM();
                ProductVM.MapToViewModel(product);
            }
            else
                ProductVM = new ProductVM { Id = 0 };


            PopulateSelections();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateSelections();
                return Page();
            }

            if (ProductVM.Id != 0)
            {
                var product = await _context.Product.FindAsync(ProductVM.Id);
                if (product == null)
                    return NotFound();

                ProductVM.MapToDomain(product);                
            }
            else
            {
                var product = new Product();
                ProductVM.MapToDomain(product);
               _context.Product.Add(product);
               
            }           

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }

        private async void PopulateSelections()
        {
            Brands = new List<string>(await _context.Product.OrderBy(x => x.Brand).Select(x => x.Brand).Distinct().ToListAsync());
            Categories = new SelectList(await _context.ProductCategory.ToListAsync(), "Id", "Title");
        }
    }

    public class ProductVM
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(140, MinimumLength = 2)]
        public string Title { get; set; } = String.Empty;

        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; } = String.Empty;

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Brand { get; set; } = String.Empty;
        
        public decimal Price { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Available")]
        public int AvailableQty { get; set; }

        [Display(Name = "Category")]
        public int? ProductCategoryId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; } = String.Empty;

        public void MapToDomain(Product product)
        {
            product.Title = Title;
            product.Description = Description;
            product.Brand = Brand;
            product.Price = Price;
            product.AvailableQty = AvailableQty;
            product.ProductCategoryId = ProductCategoryId;
            product.IsActive = IsActive;
            product.ModelNumber = ModelNumber;
        }

        public void MapToViewModel(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            Brand = product.Brand;
            Price = product.Price;
            AvailableQty = product.AvailableQty;
            ProductCategoryId = product.ProductCategoryId;
            IsActive = product.IsActive;
            ModelNumber = product.ModelNumber;
        }
    }
}
