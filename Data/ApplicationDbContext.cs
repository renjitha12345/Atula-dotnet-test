using AtulaHomeFurniture.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtulaHomeFurniture.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Table" },
                new Category { Id = 2, Name = "Chair" },
                new Category { Id = 3, Name = "Sofa" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Sku = "SKUA", Name = "Lorem Table" },
                new Product { Id = 2, Sku = "SKUB", Name = "Ipsum Table" },
                new Product { Id = 3, Sku = "SKUC", Name = "Dolor Table" },
                new Product { Id = 4, Sku = "SKUD", Name = "Sit Chair" },
                new Product { Id = 5, Sku = "SKUE", Name = "Amet Chair" },
                new Product { Id = 6, Sku = "SKUF", Name = "Consectetur Chair" },
                new Product { Id = 7, Sku = "SKUG", Name = "Adipiscing Sofa" },
                new Product { Id = 8, Sku = "SKUH", Name = "Elit Sofa" },
                new Product { Id = 9, Sku = "SKUI", Name = "Mauris Sofa" }
            );

            // Define many-to-many relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(j => j.HasData(
                    new { ProductsId = 1, CategoriesId = 1 }, // Lorem Table -> Table
                    new { ProductsId = 2, CategoriesId = 1 }, // Ipsum Table -> Table
                    new { ProductsId = 3, CategoriesId = 1 }, // Dolor Table -> Table
                    new { ProductsId = 4, CategoriesId = 2 }, // Sit Chair -> Chair
                    new { ProductsId = 5, CategoriesId = 2 }, // Amet Chair -> Chair
                    new { ProductsId = 6, CategoriesId = 2 }, // Consectetur Chair -> Chair
                    new { ProductsId = 7, CategoriesId = 3 }, // Adipiscing Sofa -> Sofa
                    new { ProductsId = 8, CategoriesId = 3 }, // Elit Sofa -> Sofa
                    new { ProductsId = 9, CategoriesId = 3 }  // Mauris Sofa -> Sofa
                ));
        }

    }
}
