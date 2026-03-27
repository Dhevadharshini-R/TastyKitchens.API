using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Helpers;

namespace TastyKitchens.API.Services;

public class OrderService
{
    private readonly string ordersPath;
    private readonly string foodItemsPath;
    private readonly string usersPath; // 🔥 ADD

    public OrderService()
    {
        var basePath = Directory.GetCurrentDirectory();

        ordersPath = Path.Combine(basePath, "Data", "orders.json");
        foodItemsPath = Path.Combine(basePath, "Data", "fooditems.json");
        usersPath = Path.Combine(basePath, "Data", "users.json"); // 🔥 ADD
    }

    // ✅ PLACE ORDER
    public Order PlaceOrder(CreateOrderRequest request, string userId)
    {
        var foodItems = FileHelper.ReadFromFile<FoodItem>(foodItemsPath);

        // 🔥 ADD: user profile fetch
        var users = FileHelper.ReadFromFile<User>(usersPath);
        var user = users.FirstOrDefault(u => u.Email == userId);

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var item in request.Items)
        {
            var food = foodItems.FirstOrDefault(f => f.Id == item.FoodItemId);

            if (food == null)
                throw new Exception($"Food item {item.FoodItemId} not found");

            var itemTotal = food.Cost * item.Quantity;
            totalAmount += itemTotal;

            orderItems.Add(new OrderItem
            {
                FoodItemId = food.Id,
                Quantity = item.Quantity,
                UnitCost = food.Cost
            });
        }

        // 🔥 UPDATED LOGIC (PROFILE + OVERRIDE + TEMP)
        var finalAddress = !string.IsNullOrWhiteSpace(request.Address)
            ? request.Address
            : user?.Address ?? "Chennai, T Nagar";

        var finalPhone = !string.IsNullOrWhiteSpace(request.PhoneNumber)
            ? request.PhoneNumber
            : user?.PhoneNumber ?? "9876543210";

        var newOrder = new Order
{
    Id = $"ORD{DateTime.Now.Ticks}",
    UserId = userId,

    PhoneNumber = finalPhone,
    Address = finalAddress,

    RestaurantId = request.RestaurantId, // 🔥 ADD THIS

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

    // ✅ GET MY ORDERS (USER)
    public List<Order> GetMyOrders(string userId)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        return orders.Where(o => o.UserId == userId).ToList();
    }

    // ✅ GET ALL ORDERS (ADMIN)
   public List<Order> GetAllOrders(string userEmail, string role)
{
    var orders = FileHelper.ReadFromFile<Order>(ordersPath);

    if (role == "SuperAdmin")
        return orders;

    if (role == "Admin")
    {
        var adminRestaurantId = "1"; // map later properly
        return orders.Where(o => o.RestaurantId == adminRestaurantId).ToList();
    }

    return new List<Order>();
}

    // ✅ GET ORDER BY ID
    public Order GetOrderById(string orderId)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        return orders.FirstOrDefault(o => o.Id == orderId);
    }

    // ✅ UPDATE ORDER STATUS (ADMIN)
    public Order UpdateOrderStatus(string orderId, string newStatus)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);

        var order = orders.FirstOrDefault(o => o.Id == orderId);

        if (order == null)
            throw new Exception("Order not found");

        if (!IsValidStatusTransition(order.Status, newStatus))
            throw new Exception("Invalid status transition");

        order.Status = newStatus;

        FileHelper.WriteToFile(ordersPath, orders);

        return order;
    }

    // ✅ STATUS FLOW VALIDATION
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