using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Data;
using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommerceRazorDemo.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly CommerceRazorDemo.Data.CommerceRazorDemoContext _context;

        public IndexModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;
        public SelectList? Brands { get; set; } = default!;
        public SelectList? Categories { get; set; } = default!;

        public string TitleSort { get; set; } = string.Empty;
        public string BrandSort { get; set; } = string.Empty;
        public string CategorySort { get; set; } = string.Empty;       
        public string PriceSort { get; set; } = string.Empty;
        public string QuantitySort { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BrandFilterString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryFilterId { get; set; }

        

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.Product != null)
            {
                var productsQuery = from p in _context.Product select p;

                if (!string.IsNullOrEmpty(SearchString))
                    productsQuery = productsQuery.Where(p => p.Title.Contains(SearchString));

                if(!string.IsNullOrEmpty(BrandFilterString))
                    productsQuery = productsQuery.Where(p => p.Brand == BrandFilterString);

                if (CategoryFilterId.HasValue)
                    productsQuery = productsQuery.Where(p => p.ProductCategoryId == CategoryFilterId);

                

                TitleSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                BrandSort = sortOrder == "brand" ? "brand_desc" : "brand";
                CategorySort = sortOrder == "cat" ? "cat_desc" : "cat";               
                PriceSort = sortOrder == "price" ? "price_desc" : "price";
                QuantitySort = sortOrder == "qty" ? "qty_desc" : "qty";
                switch (sortOrder)
                {
                    case "name_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.Title);
                        break;

                    case "brand":
                        productsQuery = productsQuery.OrderBy(p => p.Brand);
                        break;

                    case "brand_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.Brand);
                        break;

                    case "cat":
                        productsQuery = productsQuery.OrderBy(p => p.ProductCategory.Title);
                        break;

                    case "cat_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.ProductCategory.Title);
                        break;
                                            
                    case "price":
                        productsQuery = productsQuery.OrderBy(p => p.Price);
                        break;

                    case "price_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.Price);
                        break;

                    case "qty":
                        productsQuery = productsQuery.OrderBy(p => p.AvailableQty);
                        break;

                    case "qty_desc":
                        productsQuery = productsQuery.OrderByDescending(p => p.AvailableQty);
                        break;

                    default:
                        productsQuery = productsQuery.OrderBy(p => p.Title);
                        break;
                }
                

                Product = await productsQuery
                    .AsNoTracking()
                    .Include(x => x.ProductCategory)
                    .ToListAsync();


                Brands = new SelectList(await _context.Product.OrderBy(x => x.Brand).Select(x => x.Brand).Distinct().ToListAsync());
                Categories = new SelectList(await _context.ProductCategory.ToListAsync(), "Id", "Title");
            }
        }
    }
}
