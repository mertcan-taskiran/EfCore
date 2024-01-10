using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Adding Context Class
public class ShopContext: DbContext
{
    public DbSet<Product> Products {get;set;}
    public DbSet<Category> Categories {get;set;}

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); }); // LINQ to SQL at Console
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {      
        optionsBuilder
        .UseLoggerFactory(MyLoggerFactory)
        .UseSqlite("Data Source=shop.db");
    }
}

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
    // Primary Key
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}

class Program
{
    static void AddProducts()
    {
        using (var db = new ShopContext())
        {
            var products = new List<Product>()
            {
                new Product { Name = "IPhone 12", Price = 3000 },
                new Product { Name = "IPhone 13", Price = 4000 },
                new Product { Name = "IPhone 14", Price = 5000 },
            };

            db.Products.AddRange(products); // AddRange for Collection
            db.SaveChanges();   
            Console.WriteLine("Data Added.");   
        }
    }

    static void AddProduct()
    {
        using (var db = new ShopContext())
        {
            var p = new Product { Name = "IPhone 12", Price = 3000 };
            db.Products.Add(p); // AddRange for Collection
            db.SaveChanges();   
            Console.WriteLine("Data Added.");   
        }
    }

    static void Main()
    {
        AddProduct();
    }
}
