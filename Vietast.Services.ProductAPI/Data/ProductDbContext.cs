using Microsoft.EntityFrameworkCore;
using Vietast.Services.ProductAPI.Models;

namespace Vietast.Services.ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SKU> SKUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<SKU>()
                .HasOne(s => s.Product)
                .WithMany(p => p.SKUs)
                .HasForeignKey(s => s.ProductId);
        }
    }
}
