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
    }
}
