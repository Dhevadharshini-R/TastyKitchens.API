using System.Text.Json;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Data;

public static partial class FakeDb
{
    public static List<Restaurant> Restaurants = new List<Restaurant>();
    public static List<FoodItem> FoodItems = new List<FoodItem>();
    public static List<User> Users = new List<User>();

    static FakeDb()
    {
        LoadData();
    }

    private static void LoadData()
    {
        try
        {
            string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string usersPath = Path.Combine(dataPath, "users.json");

            if (File.Exists(usersPath))
            {
                var json = File.ReadAllText(usersPath);
                Users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            

            string foodItemsPath = Path.Combine(dataPath, "foodItems.json");
            string restaurantsPath = Path.Combine(dataPath, "restaurants.json");

            if (File.Exists(foodItemsPath))
            {
                var json = File.ReadAllText(foodItemsPath);
                FoodItems = JsonSerializer.Deserialize<List<FoodItem>>(json) ?? new List<FoodItem>();
            }

            if (File.Exists(restaurantsPath))
            {
                var json = File.ReadAllText(restaurantsPath);
                Restaurants = JsonSerializer.Deserialize<List<Restaurant>>(json) ?? new List<Restaurant>();
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

            var json = JsonSerializer.Serialize(FoodItems, new JsonSerializerOptions
            {
                WriteIndented = true
            });

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

        var json = JsonSerializer.Serialize(Restaurants, new JsonSerializerOptions
        {
            WriteIndented = true
        });

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

            var json = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });

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
}
