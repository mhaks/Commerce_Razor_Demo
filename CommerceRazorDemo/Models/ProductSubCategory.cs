using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CommerceRazorDemo.Models
{
    public class ProductSubCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        public int? ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;

    }
}
