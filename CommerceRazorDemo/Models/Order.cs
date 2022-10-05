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
       
        public ICollection<OrderProduct> Products { get; set; } = null!;

        public ICollection<OrderHistory> OrderHistory { get; set; } = null!;

        [Display(Name ="Sub Total")]
        public decimal Subtotal { 
            get
            {
                decimal subtotal = 0;
                foreach(var item in Products)
                {
                    subtotal += item.Price;

                }
                return subtotal;
            }                
        }

        [Display(Name = "Tax")]
        public decimal Tax
        {
            get
            {
                return Subtotal * Customer.StateLocation.TaxRate;
            }
        }

        [Display(Name = "Total Price")]
        public decimal TotalPrice
        {
            get
            {
                return Subtotal + Tax;
            }
        }


       
    }
}