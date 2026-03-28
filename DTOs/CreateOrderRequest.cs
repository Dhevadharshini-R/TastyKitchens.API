namespace TastyKitchens.API.DTOs;

public class CreateOrderRequest
{
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public int RestaurantId { get; set; }

    public List<CreateOrderItem> Items { get; set; } = new();
}

public class CreateOrderItem
{
    public int FoodItemId { get; set; }

    public int Quantity { get; set; }
}