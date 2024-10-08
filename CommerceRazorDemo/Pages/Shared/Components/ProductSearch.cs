﻿using CommerceRazorDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommerceRazorDemo.Pages.Shared.Components
{
    public class ProductSearchViewComponent : ViewComponent
    {

        public ProductSearchViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(string? searchTerm)
        {
            var context = ViewData["DbContext"] as CommerceRazorDemo.Data.CommerceRazorDemoContext;

            List<Models.ProductCategory> categories;

            if (context != null)
            {
                categories = await context.ProductCategory
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ToListAsync();
            }
            else
            {
                categories = new List<Models.ProductCategory>();
            }
 

            return View(new SearchViewModel { Categories = categories, SearchTerm = searchTerm ?? string.Empty});
        }        
    }

    public class SearchViewModel
    {
        public required List<Models.ProductCategory> Categories { get; set; }
        public required string SearchTerm { get; set; }
    }
}
