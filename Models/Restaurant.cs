namespace TastyKitchens.API.Models;

public class Restaurant
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string Cuisine { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int CostForTwo { get; set; }

    public string DeliveryTime { get; set; } = string.Empty;

    public string Distance { get; set; } = string.Empty;

    public bool IsOpen { get; set; }

    public double Rating { get; set; }

    public int TotalReviews { get; set; }
}