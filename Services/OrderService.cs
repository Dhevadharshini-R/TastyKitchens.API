using TastyKitchens.API.Data;
using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Services;

public class OrderService
{
   public Order PlaceOrder(CreateOrderRequest request, string userEmail)
    {
        // 🔥 STEP 1: CHECK RESTAURANT STATUS
        var restaurant = FakeDb.Restaurants
            .FirstOrDefault(r =>r.Id == request.RestaurantId);

        if (restaurant == null)
            throw new Exception("Restaurant not found");

        if (!restaurant.IsOpen)
            throw new Exception("Restaurant is currently closed");

        // 🔥 STEP 2: CONTINUE ORDER
        var nextId = "ORD" + DateTime.Now.Ticks.ToString().Substring(10);
        
        var order = new Order
        {
            Id = nextId,
            UserId = userEmail,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
           RestaurantId = request.RestaurantId,
            OrderDate = DateTime.Now,
            Status = "Placed",
            Items = request.Items.Select(i => {
                var foodItem = FakeDb.FoodItems.FirstOrDefault(f => f.Id == i.FoodItemId);
                return new OrderItem
                {
                    FoodItemId = i.FoodItemId,
                    Quantity = i.Quantity,
                    UnitPrice = foodItem?.Price ?? 0,   
                    FoodName = foodItem?.Name ?? "Unknown"
                };
            }).ToList()
        };

        order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        FakeDb.Orders.Add(order);
        FakeDb.SaveOrdersToFile();

        return order;
}

   public List<Order> GetMyOrders(string email)
{
    return FakeDb.Orders
        .Where(o => o.UserId.Equals(email, StringComparison.OrdinalIgnoreCase))
        .OrderByDescending(o => o.OrderDate)   
        .ToList();
}

  public List<Order> GetAllOrders(string email, string role)
{
    var orders = FakeDb.Orders;

    if (role == "SuperAdmin")
    {
        return orders;
    }

    if (role == "Admin")
    {
        var user = FakeDb.Users
            .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        var adminRestaurantId = user?.RestaurantId ?? 0;

        return orders
            .Where(o => o.RestaurantId == adminRestaurantId)
            .ToList();
    }

    return new List<Order>();
}

    public Order UpdateOrderStatus(string orderId, string status)
    {
        var order = FakeDb.Orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null) throw new Exception("Order not found");

        order.Status = status;
        FakeDb.SaveOrdersToFile();

        return order;
    }
}
