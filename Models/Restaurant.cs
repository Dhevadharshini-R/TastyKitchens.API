namespace TastyKitchens.API.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Cuisine { get; set; }
    public string Location { get; set; }
    public int CostForTwo { get; set; }
    public string DeliveryTime { get; set; }
    public string Distance { get; set; }
    public bool IsOpen { get; set; }
    public double Rating { get; set; }
    public int TotalReviews { get; set; }
}
