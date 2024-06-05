using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Data
{
    public class ShoppingDbContext:IdentityDbContext<AppUser>
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext>options):base(options) 
        {
            
        }
        public DbSet<Brand> Brands { get; set; }    
        public DbSet<Category>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Brand>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(p=>p.Products).HasForeignKey(p=>p.CategoryId);
            modelBuilder.Entity<Product>().HasOne(p => p.Brand).WithMany(p => p.Products).HasForeignKey(p => p.BrandId);

        }
    }
}
