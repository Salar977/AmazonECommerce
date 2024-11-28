using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonECommerce.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MinLength(3), MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(5), MaxLength(100)]
    public string? Description { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Image URL cannot exceed 1000 characters.")]
    [Url(ErrorMessage = "Image URL must be a valid URL.")]
    [Required(ErrorMessage = "Image URL is required.")]
    public string ImageUrl { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    [Range(0, 100000, ErrorMessage = "Price must be between 0 to 100,000.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
    public int Quantity { get; set; }

    [Required]
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }


    // Navigation Properties
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }

}