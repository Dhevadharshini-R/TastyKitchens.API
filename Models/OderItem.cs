namespace TastyKitchens.API.Models;
 
public class OrderItem
{
    public int FoodItemId { get; set; }
 
    public int Quantity { get; set; }
 
    public decimal UnitPrice { get; set; }
    public string FoodName { get; set; } = "";
}