using System.Text.Json;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Data;

public static partial class FakeDb
{
    public static List<Restaurant> Restaurants = new List<Restaurant>();
    public static List<FoodItem> FoodItems = new List<FoodItem>();
    public static List<User> Users = new List<User>();
    public static List<Order> Orders = new List<Order>();

    static FakeDb()
    {
        LoadData();
    }

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    private static void LoadData()
    {
        try
        {
            string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string usersPath = Path.Combine(dataPath, "users.json");

            if (File.Exists(usersPath))
            {
                var json = File.ReadAllText(usersPath);
                Users = JsonSerializer.Deserialize<List<User>>(json, _jsonOptions) ?? new List<User>();
            }

            string ordersPath = Path.Combine(dataPath, "orders.json");
            if (File.Exists(ordersPath))
            {
                var json = File.ReadAllText(ordersPath);
                Orders = JsonSerializer.Deserialize<List<Order>>(json, _jsonOptions) ?? new List<Order>();
            }
            

            string foodItemsPath = Path.Combine(dataPath, "foodItems.json");
            string restaurantsPath = Path.Combine(dataPath, "restaurants.json");

            if (File.Exists(foodItemsPath))
            {
                var json = File.ReadAllText(foodItemsPath);
                FoodItems = JsonSerializer.Deserialize<List<FoodItem>>(json, _jsonOptions) ?? new List<FoodItem>();
            }

            if (File.Exists(restaurantsPath))
            {
                var json = File.ReadAllText(restaurantsPath);
                Restaurants = JsonSerializer.Deserialize<List<Restaurant>>(json, _jsonOptions) ?? new List<Restaurant>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }

    private static void SaveFoodItems()
    {
        try
        {
            string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            string foodItemsPath = Path.Combine(dataPath, "foodItems.json");

            var json = JsonSerializer.Serialize(FoodItems, _jsonOptions);

            File.WriteAllText(foodItemsPath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving food items: {ex.Message}");
        }
    }

    public static void SaveFoodItemsToFile()
    {
        SaveFoodItems();
    }

    private static void SaveRestaurants()
    {
    try
    {
        string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        string restaurantsPath = Path.Combine(dataPath, "restaurants.json");

        var json = JsonSerializer.Serialize(Restaurants, _jsonOptions);

        File.WriteAllText(restaurantsPath, json);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving restaurants: {ex.Message}");
    }
    }

    public static void SaveRestaurantsToFile()
    {
        SaveRestaurants();
    }

    private static void SaveUsers()
    {
        try
        {
            string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            string usersPath = Path.Combine(dataPath, "users.json");

            var json = JsonSerializer.Serialize(Users, _jsonOptions);

            File.WriteAllText(usersPath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving users: {ex.Message}");
        }
    }

    public static void SaveUsersToFile()
    {
        SaveUsers();
    }

    private static void SaveOrders()
    {
        try
        {
            string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            string ordersPath = Path.Combine(dataPath, "orders.json");
            var json = JsonSerializer.Serialize(Orders, _jsonOptions);
            File.WriteAllText(ordersPath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving orders: {ex.Message}");
        }
    }

    public static void SaveOrdersToFile()
    {
        SaveOrders();
    }
}
