using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Models
{
    
    public class OrderStatus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = String.Empty;
    }

    public enum OrderState
    {
         Cart = 1,
         Processing = 2,
         Cancelled = 3,
         Shipped = 4,
         Delivered = 5,
         Returned = 6,                      
    }
}
