using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using J89582.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace J89582.Data
{
    public class J89582Context : IdentityDbContext<ApplicationUser>
    {
        public J89582Context(DbContextOptions<J89582Context> options)
            : base(options)
        {
        }

        public DbSet<J89582.Model.Menu> Menu { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
