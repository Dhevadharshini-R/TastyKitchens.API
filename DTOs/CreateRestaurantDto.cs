namespace TastyKitchens.API.DTOs;

public class CreateRestaurantDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Cuisine { get; set; }
    public string Location { get; set; }
    public int CostForTwo { get; set; }
}
