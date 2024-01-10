using System;
using System.ComponentModel.DataAnnotations;

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
        Console.WriteLine("Merhaba, dünya!");
    }
}
