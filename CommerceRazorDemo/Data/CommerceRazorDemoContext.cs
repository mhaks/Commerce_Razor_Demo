using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommerceRazorDemo.Models;

namespace CommerceRazorDemo.Data
{
    public class CommerceRazorDemoContext : DbContext
    {
        public CommerceRazorDemoContext (DbContextOptions<CommerceRazorDemoContext> options)
            : base(options)
        {
        }

        public DbSet<CommerceRazorDemo.Models.Product> Product { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.ProductCategory> ProductCategory { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.ProductSubCategory> ProductSubCategory { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.Customer> Customer { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.StateLocation> StateLocation { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.Order> Order { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.OrderProduct> OrderProduct { get; set; } = default!;
        public DbSet<CommerceRazorDemo.Models.OrderStatus> OrderStatus { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommerceRazorDemo.Models.OrderStatus>().ToTable("OrderStatus");
        }
    }
}
