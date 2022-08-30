﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
