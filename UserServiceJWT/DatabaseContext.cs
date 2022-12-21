using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserServiceJWT.Entities;

namespace UserServiceJWT
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>().HasMany(list => list.orderedProducts).WithOne().HasForeignKey(fk => fk.order_id);
            builder.Entity<User>().HasMany(field => field.orders).WithOne().HasForeignKey(fk => fk.user_id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }


    }
}