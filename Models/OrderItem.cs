namespace TastyKitchens.API.Models;

public class OrderItem
{
    public string FoodItemId { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}