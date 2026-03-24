namespace TastyKitchens.API.DTOs;

public class CreateRestaurantDto
{
    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Cuisine { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int CostForTwo { get; set; }
}