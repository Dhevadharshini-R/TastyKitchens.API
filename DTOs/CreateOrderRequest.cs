namespace TastyKitchens.API.DTOs;

public class CreateOrderRequest
{
    public string UserId { get; set; } = string.Empty;

    public List<CreateOrderItem> Items { get; set; } = new();
}

public class CreateOrderItem
{
    public string FoodItemId { get; set; } = string.Empty;

    public int Quantity { get; set; }
}