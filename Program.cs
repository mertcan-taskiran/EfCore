using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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
    static void Main()
    {
        using (var db = new ShopContext())
        {
            var p = new Product {Name = "Monster Notebook", Price=30000};  
            db.Products.Add(p);
            db.SaveChanges();   
            Console.WriteLine("Veriler Eklendi...");   
        }
    }
}
