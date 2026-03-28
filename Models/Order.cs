namespace TastyKitchens.API.Models;


public class Order
{
    public string Id { get; set; } = string.Empty;
    

    public string UserId { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = "Placed";

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public List<OrderItem> Items { get; set; } = new();
}