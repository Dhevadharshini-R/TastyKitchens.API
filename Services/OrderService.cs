using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Helpers;

namespace TastyKitchens.API.Services;

public class OrderService
{
    private readonly string ordersPath;
    private readonly string foodItemsPath;
    private readonly string restaurantsPath;

    public OrderService()
    {
        var basePath = Directory.GetCurrentDirectory();

        ordersPath = Path.Combine(basePath, "Data", "orders.json");
        foodItemsPath = Path.Combine(basePath, "Data", "fooditems.json");
        restaurantsPath = Path.Combine(basePath, "Data", "restaurants.json");
    }

    // ✅ PLACE ORDER (FIXED)
    public Order PlaceOrder(CreateOrderRequest request, string email)
    {
        var foodItems = FileHelper.ReadFromFile<FoodItem>(foodItemsPath);
        var restaurants = FileHelper.ReadFromFile<Restaurant>(restaurantsPath);

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        // 🔥 ADDITION 1: SINGLE RESTAURANT VALIDATION
        string? restaurantId = null;

        foreach (var item in request.Items)
        {
            var food = foodItems.FirstOrDefault(f => f.Id == item.FoodItemId);

            if (food == null)
                throw new Exception($"Food item {item.FoodItemId} not found");

            var restaurant = restaurants.FirstOrDefault(r => r.Id.ToString() == food.RestaurantId);

            if (restaurant == null)
                throw new Exception("Restaurant not found");

            // 🔥 ADDITION 2: CLOSED RESTAURANT CHECK (ALREADY YOUR LOGIC - ENHANCED MESSAGE)
            if (!restaurant.IsOpen)
                throw new Exception($"❌ Cannot place order. Restaurant '{restaurant.Name}' is currently closed.");

            // 🔥 ADDITION 3: MULTIPLE RESTAURANT BLOCK
            if (restaurantId == null)
            {
                restaurantId = food.RestaurantId;
            }
            else if (restaurantId != food.RestaurantId)
            {
                throw new Exception("❌ Cannot order from multiple restaurants in a single order.");
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
            UserId = email, // 🔥 FIX
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

    public List<Order> GetUserOrders(string userId)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        return orders.Where(o => o.UserId == userId).ToList();
    }

    public List<Order> GetAllOrders()
    {
        return FileHelper.ReadFromFile<Order>(ordersPath);
    }

    public Order UpdateOrderStatus(string orderId, string newStatus)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);

        var order = orders.FirstOrDefault(o => o.Id == orderId);

        if (order == null)
            throw new Exception("Order not found");

        if (order.Status == "Delivered")
            throw new Exception("Order already delivered. Cannot update.");

        if (!IsValidStatusTransition(order.Status, newStatus))
            throw new Exception($"Invalid status transition from {order.Status} to {newStatus}");

        order.Status = newStatus;

        FileHelper.WriteToFile(ordersPath, orders);

        return order;
    }

    private bool IsValidStatusTransition(string current, string next)
    {
        return current switch
        {
            "Placed" => next == "Preparing" || next == "Cancelled",
            "Preparing" => next == "Delivered" || next == "Cancelled",
            _ => false
        };
    }

    // ✅ ADMIN FILTER
    public List<Order> GetOrdersByAdmin(string adminEmail)
    {
        var orders = FileHelper.ReadFromFile<Order>(ordersPath);
        var foodItems = FileHelper.ReadFromFile<FoodItem>(foodItemsPath);
        var restaurants = FileHelper.ReadFromFile<Restaurant>(restaurantsPath);

        var adminRestaurantIds = restaurants
            .Where(r => r.AdminEmail == adminEmail)
            .Select(r => r.Id.ToString())
            .ToList();

        var result = new List<Order>();

        foreach (var order in orders)
        {
            foreach (var item in order.Items)
            {
                var food = foodItems.FirstOrDefault(f => f.Id == item.FoodItemId);

                if (food != null && adminRestaurantIds.Contains(food.RestaurantId))
                {
                    result.Add(order);
                    break;
                }
            }
        }

        return result;
    }
}