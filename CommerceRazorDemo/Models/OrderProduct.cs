using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int Quantity { get; set; }   

        public int? ProductId { get; set; }

        public int? OrderId { get; set; }

        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
