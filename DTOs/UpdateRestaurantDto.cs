using System.ComponentModel.DataAnnotations;

namespace TastyKitchens.API.DTOs;

public class UpdateRestaurantDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    [Required]
    public string Cuisine { get; set; }

    [Required]
    public string Location { get; set; }

    [Range(1, 10000)]
    public int CostForTwo { get; set; }
}
