using CommerceDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CommerceRazorDemo.Pages.Customers
{
    [Authorize(Roles = "ADMIN")]
    public class EditModel : CommerceDemoPageModel
    {

        public EditModel(CommerceDemo.Data.CommerceDemoContext context, ILogger<EditModel> logger)
             : base(context, logger)
        {

        }


        public SelectList UsStates { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CustomerId { get; set; }

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
        [Required]
        [Display(Name = "State")]
        public int StateLocationId { get; set; }

        [BindProperty]
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


        public async Task<IActionResult> OnGetAsync()
        {
            if (_context == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(CustomerId))
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == CustomerId);
                if (user == null)
                {
                    return NotFound();
                }

                CustomerId = user.Id;
                UserName = user.UserName ?? String.Empty;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Address1 = user.Address1;
                Address2 = user.Address2;
                City = user.City;
                StateLocationId = user.StateLocationId;
                PostalCode = user.PostalCode;
                PhoneNumber = user.PhoneNumber ?? String.Empty;
                EmailAddress = user.Email ?? string.Empty;
            }
            else
            {
                var state = await _context.StateLocation.OrderBy(s => s.Abbreviation).FirstAsync();
                StateLocationId = state.Id;
            }

            await LoadSelections();
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
                await LoadSelections();
                return Page();
            }

            if (!String.IsNullOrEmpty(CustomerId))
            {
                var customer = await _context.Users.FindAsync(CustomerId);
                if (customer == null)
                    return NotFound();

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
                if (await _context.Users.AnyAsync(u => u.UserName == UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    await LoadSelections();
                    return Page();
                }

                UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(_context);
                var hasher = new PasswordHasher<IdentityUser>();

                var customer = new ApplicationUser
                {
                    UserName = UserName,
                    NormalizedUserName = UserName.ToUpper(),
                    FirstName = FirstName,
                    LastName = LastName,
                    Address1 = Address1,
                    Address2 = Address2,
                    City = City,
                    StateLocationId = StateLocationId,
                    PostalCode = PostalCode,
                    PhoneNumber = PhoneNumber,
                    Email = EmailAddress,
                    NormalizedEmail = EmailAddress.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                customer.PasswordHash = hasher.HashPassword(customer, "password");
                await userStore.CreateAsync(customer);
                await userStore.AddToRoleAsync(customer, "CUSTOMER");
            }

            try
            {
                await _context.SaveChangesAsync();                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!String.IsNullOrEmpty(CustomerId) && !CustomerExists(CustomerId))
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

        private async Task LoadSelections()
        {
            UsStates = new SelectList(await _context.StateLocation.ToListAsync(), "Id", "Abbreviation");
        }
        
    }
       
}
