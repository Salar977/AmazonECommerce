using System.ComponentModel.DataAnnotations;

namespace AmazonECommerce.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }


    // Navigation Properties
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }

}