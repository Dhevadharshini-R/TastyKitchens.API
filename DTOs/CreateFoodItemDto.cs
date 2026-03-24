namespace TastyKitchens.API.DTOs;

public class CreateFoodItemDto
{
    public string RestaurantId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}