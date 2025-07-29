using Microsoft.EntityFrameworkCore;
using CMS.Models;
using System.Collections.Generic;
namespace CMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        //public DbSet<Users> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Инициализация начальных данных
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin", Email = "admin@example.com" },
                new User { Id = 3, Username = "user", Password = "user123", Role = "User", Email = "user@example.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Samsung", Price = 75000},
                new Product { Id = 2, Name = "IPhone", Price = 65000 },
                new Product { Id = 3, Name = "Nokia", Price = 50000 }
            );
        }
    }
}
