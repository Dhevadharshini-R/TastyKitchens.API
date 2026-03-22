namespace TastyKitchens.API.DTOs;

public class CreateFoodItemDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public string ImageUrl { get; set; }
}
