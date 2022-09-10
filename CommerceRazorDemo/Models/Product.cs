using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Brand { get; set; } = String.Empty;

        [Required]
        [StringLength(140, MinimumLength = 3)]
        public string Title { get; set; } = String.Empty;

        [Required]
        [StringLength(1000, MinimumLength = 3)] 
        public string Description { get; set; } = String.Empty;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "In Stock")]
        public int AvailableQty { get; set; }

        public int? ProductCategoryId { get; set; }
        [Display(Name = "Category")]
        public ProductCategory ProductCategory { get; set; } = null!;

        public int? ProductSubCategoryId { get; set; }
        [Display(Name = "Subcategory")]
        public ProductSubCategory ProductSubCategory { get; set; } = null!;
    }
}
