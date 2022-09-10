using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommerceRazorDemo.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int? OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; } = null!;
        
        public ICollection<OrderProduct> Products { get; set; } = null!;

        public decimal Subtotal { 
            get
            {
                decimal subtotal = 0;
                foreach(var item in Products)
                {
                    subtotal += item.Product.Price;

                }
                return subtotal;
            }                
        }

        public decimal Tax
        {
            get
            {
                return Subtotal * Customer.StateLocation.TaxRate;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return Subtotal + Tax;
            }
        }


       
    }
}