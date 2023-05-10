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
using CommerceRazorDemo.Migrations;
using CommerceRazorDemo.Pages.Products;

namespace CommerceRazorDemo.Pages.Customers
{
    public class EditModel : CommerceDemoPageModel
    {
        
        public EditModel(CommerceRazorDemo.Data.CommerceRazorDemoContext context, ILogger<EditModel> logger)
             : base(context, logger)
        {

        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; } = default!;

        public SelectList UsStates { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (Context.Customer == null)
            {
                return NotFound();
            }

            if (id.HasValue && id.Value != 0)
            {
                var customer = await Context.Customer.FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }
                Customer = new CustomerViewModel();
                Customer.MapToViewModel(customer);
            }
            else
            {
                var states = await Context.StateLocation.OrderBy(s => s.Abbreviation).ToListAsync();
                Customer = new CustomerViewModel { Id = 0, StateLocationId = states.First().Id };
            }

            LoadSelections();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelections();
                return Page();
            }

            if (Customer.Id != 0)
            {
                var cust = await Context.Customer.FindAsync(Customer.Id);
                if (cust == null)
                    return NotFound();

                Customer.MapToDomain(cust);
            }
            else
            {
                var cust = new Customer();
                Customer.MapToDomain(cust);
                Context.Customer.Add(cust);
            }



            try
            {
                await Context.SaveChangesAsync();                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
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

        private bool CustomerExists(int id)
        {
          return (Context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void LoadSelections()
        {
            UsStates = new SelectList(await Context.StateLocation.ToListAsync(), "Id", "Abbreviation");
        }

       

    }

    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Street Address")]
        public string Address1 { get; set; } = string.Empty;


        [StringLength(100)]
        [Display(Name = "Additional Address")]
        public string? Address2 { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; } = string.Empty;

        [Display(Name = "State")]
        public int StateLocationId { get; set; }
             
        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Zip")]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }


        public void MapToDomain(Customer customer)
        {
            customer.UserName = UserName;
            customer.FirstName = FirstName;
            customer.LastName = LastName;
            customer.Address1 = Address1;
            customer.Address2 = Address2;
            customer.City = City;
            customer.StateLocationId = StateLocationId;
            customer.PostalCode = PostalCode;
            customer.PhoneNumber = PhoneNumber;
            customer.EmailAddress = EmailAddress;
        }


        public void MapToViewModel(Customer customer)
        {
            Id = customer.Id;
            UserName = customer.UserName;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Address1 = customer.Address1;
            Address2 = customer.Address2;
            City = customer.City;
            StateLocationId = customer.StateLocationId;
            PostalCode = customer.PostalCode;
            PhoneNumber = customer.PhoneNumber;
            EmailAddress = customer.EmailAddress;
        }
    }
}
