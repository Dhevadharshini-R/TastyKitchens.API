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
        
        Users.Add(new User 
        { 
            Id = 1, 
            Username = "admin", 
            Email = "admin@test.com", 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), 
            Role = "Admin" 
        });
        Users.Add(new User 
        { 
            Id = 2, 
            Username = "superadmin", 
            Email = "super@test.com", 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), 
            Role = "SuperAdmin" 
        });
        Users.Add(new User 
        { 
            Id = 3, 
            Username = "user", 
            Email = "user@test.com", 
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), 
            Role = "User" 
        });
    }

    private static void LoadData()
    {
        try
        {
            string dataPath = Path.Combine(AppContext.BaseDirectory, "Data");
            // If running from source, look in the project directory instead
            if (!Directory.Exists(dataPath))
            {
                dataPath = "Data";
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
}
