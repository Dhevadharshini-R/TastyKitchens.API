using TastyKitchens.API.Data;
using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Services;

public class OrderService
{
    public Order PlaceOrder(CreateOrderRequest request, string userEmail)
    {
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
                    UnitPrice = foodItem?.Price ?? 0
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
        return FakeDb.Orders.Where(o => o.UserId.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Order> GetAllOrders(string email, string role)
    {
        if (role == "Admin" || role == "SuperAdmin")
        {
            return FakeDb.Orders;
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
