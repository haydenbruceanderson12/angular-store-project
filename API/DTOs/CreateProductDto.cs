using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CreateProductDto
{
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public string? PictureUrl { get; set; }

    [Required]
    public string? Type { get; set; }

    [Required]
    public string? Brand { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Need at least 1 item in stock")]
    public int QuantityInStock { get; set; }
}
