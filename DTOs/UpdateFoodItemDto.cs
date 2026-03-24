using System.ComponentModel.DataAnnotations;

namespace TastyKitchens.API.DTOs;

public class UpdateFoodItemDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 10000)]
    public int Cost { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Range(0, 5)]
    public double Rating { get; set; }
}
