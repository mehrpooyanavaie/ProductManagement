using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.ProductId, cp.CategoryId });

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.product)
                .WithMany(p => p.categoryProducts)
                .HasForeignKey(cp => cp.ProductId);

            builder.Entity<CategoryProduct>()
            .HasOne(cp => cp.category)
            .WithMany(c => c.categoryProducts)
            .HasForeignKey(cp => cp.CategoryId);
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics",
                     Description = "Electronic devices" },
                new Category { Id = 2, Name = "Books",
                     Description = "Various types of books" },
                new Category { Id = 3, Name = "Clothing",
                     Description = "Apparel and garments" });
        }
    }
}
