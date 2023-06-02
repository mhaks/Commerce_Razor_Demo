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
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Drawing.Drawing2D;

using CommerceRazorDemo.Pages.Products;

namespace CommerceRazorDemo.Pages.Customers
{
    public class EditModel : CommerceDemoPageModel
    {

        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<EditModel> logger)
             : base(context, logger)
        {

        }


        public SelectList UsStates { get; set; } = default!;

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Street Address")]
        public string Address1 { get; set; } = string.Empty;

        [BindProperty]
        [StringLength(100)]
        [Display(Name = "Additional Address")]
        public string? Address2 { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; } = string.Empty;

        [BindProperty]
        [Display(Name = "State")]
        public int StateLocationId { get; set; }


        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Zip")]
        public string PostalCode { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; } = string.Empty;


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(id))
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                UserId = user.Id;
                UserName = user.UserName;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Address1 = user.Address1;
                Address2 = user.Address2;
                City = user.City;
                StateLocationId = user.StateLocationId;
                PostalCode = user.PostalCode;
                PhoneNumber = user.PhoneNumber;
                EmailAddress = user.Email;
            }
            else
            {
                var state = await _context.StateLocation.OrderBy(s => s.Abbreviation).FirstAsync();
                StateLocationId = state.Id;
            }

            LoadSelections();
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
                LoadSelections();
                return Page();
            }

            if (!String.IsNullOrEmpty(UserId))
            {
                var customer = await _context.Users.FindAsync(UserId);
                if (customer == null)
                    return NotFound();

                customer.UserName = UserName;
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Address1 = Address1;
                customer.Address2 = Address2;
                customer.City = City;
                customer.StateLocationId = StateLocationId;
                customer.PostalCode = PostalCode;
                customer.PhoneNumber = PhoneNumber;
                customer.Email = EmailAddress;
            }
            else
            {
                var customer = new ApplicationUser
                {
                    UserName = UserName,
                    FirstName = FirstName,
                    LastName = LastName,
                    Address1 = Address1,
                    Address2 = Address2,
                    City = City,
                    StateLocationId = StateLocationId,
                    PostalCode = PostalCode,
                    PhoneNumber = PhoneNumber,
                    Email = EmailAddress
                };
                _context.Users.Add(customer);
            }



            try
            {
                await _context.SaveChangesAsync();                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(UserId))
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

        private bool CustomerExists(string id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void LoadSelections()
        {
            UsStates = new SelectList(await _context.StateLocation.ToListAsync(), "Id", "Abbreviation");
        }
        
    }
       
}
