namespace TastyKitchens.API.Models;

public class OrderItem
{
    public int FoodItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitCost { get; set; }
}