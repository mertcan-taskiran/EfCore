using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

// Adding Entity Classes
public class Product
{
    // Primary Key
    public int Id { get; set; }
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Category
{
    // Adding Context Class
    public class ShopContext: DbContext
    {
        public DbSet<Product> Products {get;set;}
        public DbSet<Category> Categories {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shop.db");
        }
    }

    // Primary Key
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {

    }
}
