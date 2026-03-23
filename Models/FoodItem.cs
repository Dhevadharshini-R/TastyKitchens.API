namespace TastyKitchens.API.Models;

public class FoodItem
{
<<<<<<< HEAD
    public string Id { get; set; } = string.Empty;

    public string RestaurantId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public double Rating { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}
=======
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public double Rating { get; set; }
    public string ImageUrl { get; set; }
}
>>>>>>> dc59b41a3bfc943efcfa648ce3a79fec71d6006f
