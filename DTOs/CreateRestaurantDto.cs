using System.ComponentModel.DataAnnotations;

namespace TastyKitchens.API.DTOs;

public class CreateRestaurantDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string ImageUrl { get; set; }

    [Required]
    public required string Cuisine { get; set; }

    [Required]
    public required string Location { get; set; }

    [Range(1, 10000)]
    public int CostForTwo { get; set; }

    public string DeliveryTime { get; set; } = "30 mins";

    public string Distance { get; set; } = "2.5 km";

    public bool IsOpen { get; set; } = true;

    [Range(0, 5)]
    public double Rating { get; set; } = 0;

    [Range(0, int.MaxValue)]
    public int TotalReviews { get; set; } = 0;
}
