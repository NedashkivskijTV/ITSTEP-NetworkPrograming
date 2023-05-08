using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public ShopContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localDb)\\MSSQLLocalDb;Database=ShopProd;Trusted_Connection=true;");
               //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ShopProdHW05;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Phone" },
                new Category { Id = 2, Name = "Notebook" },
                new Category { Id = 3, Name = "Fridge" }
            );

            modelBuilder.Entity<Product>().HasData(
            new Product[]
            {
                new Product{Id = 1, CategoryId = 1, Title = "Poco X3", Price = 7199.99},
                new Product{Id = 2, CategoryId = 1, Title = "Xiaomi", Price = 5400.99},
                new Product{Id = 3, CategoryId = 1, Title = "Huawei", Price = 3133.99},
                new Product{Id = 4, CategoryId = 2, Title = "HP ProBook 5364", Price = 16199.99},
                new Product{Id = 5, CategoryId = 3, Title = "HOLOD", Price = 13199.99},
            });

        }
    }
}