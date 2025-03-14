using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommerceRazorDemo.Pages.Products
{
    [Authorize(Roles = "ADMIN")]
    public class EditModel : CommerceDemoPageModel
    {
        public EditModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<EditModel> logger)
            : base(context, logger)
        {

        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        [Required]
        [StringLength(140, MinimumLength = 2)]
        public string Title { get; set; } = String.Empty;

        [BindProperty]
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; } = String.Empty;

        [BindProperty]
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Brand { get; set; } = String.Empty;

        [BindProperty]
        [Required]
        public decimal Price { get; set; }

        [BindProperty]
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Available")]
        public int AvailableQty { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Category")]
        public int? ProductCategoryId { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; } = String.Empty;



        public List<string> Brands { get; set; } = default!;        

        public SelectList? Categories { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (id.HasValue && id.Value != 0)
            {
                var product = await _context.Product.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

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
            else
            {
                Id = 0;
                IsActive = true;
            }
                


            await PopulateSelections();
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
                await PopulateSelections();
                return Page();
            }

            if (Id != 0)
            {
                var product = await _context.Product.FindAsync(Id);
                if (product == null)
                    return NotFound();

                product.Title = Title;
                product.Description = Description;
                product.Brand = Brand;
                product.Price = Price;
                product.AvailableQty = AvailableQty;
                product.ProductCategoryId = ProductCategoryId;
                product.IsActive = IsActive;
                product.ModelNumber = ModelNumber;

            }
            else
            {
                var product = new Product
                {
                    Title = Title,
                    Description = Description,
                    Brand = Brand,
                    Price = Price,
                    AvailableQty = AvailableQty,
                    ProductCategoryId = ProductCategoryId,
                    IsActive = IsActive,
                    ModelNumber = ModelNumber
                };

                _context.Product.Add(product);               
            }

            

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }

        private async Task PopulateSelections()
        {
            Brands = new List<string>(await _context.Product.OrderBy(x => x.Brand).Select(x => x.Brand).Distinct().ToListAsync());
            Categories = new SelectList(await _context.ProductCategory.ToListAsync(), "Id", "Title");
        }
    }

}
