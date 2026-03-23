namespace TastyKitchens.API.Models;

public class FoodItem
{
    public string Id { get; set; } = string.Empty;

    public string RestaurantId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public double Rating { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}