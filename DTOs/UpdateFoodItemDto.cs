using System.ComponentModel.DataAnnotations;

namespace TastyKitchens.API.DTOs;

public class UpdateFoodItemDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [Range(1, 10000)]
    public required int Cost { get; set; }

    [Required]
    public required string ImageUrl { get; set; }

    [Range(0, 5)]
    public double Rating { get; set; }
}
