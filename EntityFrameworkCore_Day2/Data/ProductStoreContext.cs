using Microsoft.EntityFrameworkCore;
using ProductStore.Data.Entities;

namespace ProductStore.Data;

public class ProductStoreContext : DbContext
{
    public ProductStoreContext(DbContextOptions<ProductStoreContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ConfigureTables(builder);

        ConfigureRelationships(builder);

        SeedData(builder);
    }

    private static void ConfigureTables(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .ToTable("Product")
            .HasKey(p => p.Id);

        builder.Entity<Category>()
            .ToTable("Category")
            .HasKey(c => c.Id);
    }

    private static void ConfigureRelationships(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
    }

    private static void SeedData(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Meat" },
            new Category { Id = 2, Name = "Bakery" },
            new Category { Id = 3, Name = "Beverage"}
        );

        builder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Pork", Manufacture = "VN", CategoryId = 1},
            new Product { Id = 2, Name = "Beef", Manufacture = "US", CategoryId = 1},
            new Product { Id = 3, Name = "White Bread", Manufacture = "VN", CategoryId = 2},
            new Product { Id = 4, Name = "Coke", Manufacture = "JP", CategoryId = 3},
            new Product { Id = 5, Name = "Sprite", Manufacture = "US", CategoryId = 3},
            new Product { Id = 6, Name = "Fanta", Manufacture = "VN", CategoryId = 3}
        );
    }
}