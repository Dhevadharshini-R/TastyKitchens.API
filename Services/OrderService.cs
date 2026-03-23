using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Helpers;

namespace TastyKitchens.API.Services;

public class OrderService
{
    private readonly string ordersPath;
    private readonly string foodItemsPath;

    public OrderService()
    {
        var basePath = Directory.GetCurrentDirectory();

        ordersPath = Path.Combine(basePath, "Data", "orders.json");
        foodItemsPath = Path.Combine(basePath, "Data", "fooditems.json");
    }

    // ✅ PLACE ORDER
    public Order PlaceOrder(CreateOrderRequest request)
    {
        var foodItems = FileHelper.ReadFromFile<FoodItem>(foodItemsPath);
        Console.WriteLine($"FoodItems count: {foodItems.Count}");

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var item in request.Items)
        {
            var food = foodItems.FirstOrDefault(f => f.Id == item.FoodItemId);

            if (food == null)
            {
                throw new Exception($"Food item {item.FoodItemId} not found");
            }

            var itemTotal = food.Price * item.Quantity;
            totalAmount += itemTotal;

            orderItems.Add(new OrderItem
            {
                FoodItemId = food.Id,
                Quantity = item.Quantity,
                UnitPrice = food.Price
            });
        }

        var newOrder = new Order
        {
            Id = $"ORD{DateTime.Now.Ticks}",
            UserId = request.UserId,
            TotalAmount = totalAmount,
            Status = "Placed",
            OrderDate = DateTime.Now,
            Items = orderItems
        };

        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        orders.Add(newOrder);
        FileHelper.WriteToFile(ordersPath, orders);

        return newOrder;
    }

    // ✅ GET USER ORDERS
    public List<Order> GetUserOrders(string userId)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        return orders.Where(o => o.UserId == userId).ToList();
    }

    // ✅ UPDATE ORDER STATUS
    public Order UpdateOrderStatus(string orderId, string newStatus)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);

        var order = orders.FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception("Order not found");
        }

        if (!IsValidStatusTransition(order.Status, newStatus))
        {
            throw new Exception($"Invalid status transition from {order.Status} to {newStatus}");
        }

        order.Status = newStatus;

        FileHelper.WriteToFile(ordersPath, orders);

        return order;
    }

    // ✅ VALIDATION LOGIC
    private bool IsValidStatusTransition(string current, string next)
    {
        return current switch
        {
            "Placed" => next == "Preparing" || next == "Cancelled",
            "Preparing" => next == "Delivered" || next == "Cancelled",
            "Delivered" => false,
            "Cancelled" => false,
            _ => false
        };
    }
}