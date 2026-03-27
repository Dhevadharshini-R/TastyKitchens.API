namespace TastyKitchens.API.Models;

public class FoodItem
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public double Rating { get; set; }
    public string ImageUrl { get; set; }
}
