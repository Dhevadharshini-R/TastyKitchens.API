using System.ComponentModel.DataAnnotations;

namespace TastyKitchens.API.DTOs;

public class CreateFoodItemDto
{
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 10000)]
    public int Cost { get; set; }

    [Required]
    public string ImageUrl { get; set; }
}
