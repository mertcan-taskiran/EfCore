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

    static void GetAllProducts()
    {
        using (var context = new ShopContext())
        {
            var products = context
                .Products
                .Select(p => 
                    new {
                        p.Name, p.Price
                    })
                .ToList();

            foreach (var p in products)
            {
                Console.WriteLine($"Name: {p.Name} Price: {p.Price}");
            }
        }
    }

    static void GetProductById(int id)
    {
        using (var context = new ShopContext())
        {
            var product = context
                .Products
                .Where(p => p.Id == id)
                .Select(p => 
                    new {
                        p.Name, p.Price
                    })
                .FirstOrDefault();

            Console.WriteLine($"Name: {product.Name} Price: {product.Price}");
        }
    }

    static void GetProductByName(string name)
    {
        using (var context = new ShopContext())
        {
            var products = context
                .Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(p => 
                    new {
                        p.Name, p.Price
                    })
                .ToList();

            foreach (var p in products)
            {
                Console.WriteLine($"Name: {p.Name} Price: {p.Price}");
            }
        }
    }

    static void UpdateProduct(int id)
    {   
        using(var db = new ShopContext())
        {
            // Change tracking
            var p = db.Products.Where(i=>i.Id == id).FirstOrDefault();
            if (p != null)
            {
                p.Price *= 1.5m;
                db.SaveChanges();
                Console.WriteLine("Updated.");
            }
        }
    }

    static void Main()
    {
        UpdateProduct(4);
    }
}
