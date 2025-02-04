using System.ComponentModel.DataAnnotations;

namespace pruebaCSharp.Entities;

public class Product
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    
    public int Stock { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}