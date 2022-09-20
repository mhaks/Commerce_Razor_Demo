using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Models
{
    public class Customer
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

        public int StateLocationId { get; set; }
        [Display(Name = "State")]
        public StateLocation StateLocation { get; set; } = null!;

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
    }
}
