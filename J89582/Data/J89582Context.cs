using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using J89582.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace J89582.Data
{
    public class J89582Context : IdentityDbContext<ApplicationUser>
    {
        public J89582Context(DbContextOptions<J89582Context> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; } = default!;
        public DbSet<CheckoutCust> CheckoutCust { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        
        [NotMapped]
        public DbSet<CheckoutItems> CheckoutItems { get; set; }

        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });
            builder.Entity<OrderItem>().HasKey(t => new { t.StockID, t.OrderNo });
        }

    }
}
