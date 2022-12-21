using Microsoft.EntityFrameworkCore;
using ScrantonBranch.Entities;

namespace ScrantonBranch
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>().HasMany<OrderedProductEntity>(x => x.products)
                .WithOne().HasForeignKey(fk => fk.order_id);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProductEntity> OrderedProductEntities { get; set; }

    }
}