using TastyKitchens.API.Models;
using TastyKitchens.API.Helpers;
using BCrypt.Net;

namespace TastyKitchens.API.Data;

public static partial class FakeDb
{
    private static readonly string restaurantsFile = "Data/restaurants.json";
    private static readonly string foodItemsFile = "Data/fooditems.json";
    private static readonly string usersFile = "Data/users.json";

    public static List<Restaurant> Restaurants = new List<Restaurant>();
    public static List<FoodItem> FoodItems = new List<FoodItem>();
    public static List<User> Users = new List<User>();

    static FakeDb()
    {
        // FIX: MUST USE List<T>
        Restaurants = FileHelper.ReadFromFile<Restaurant>(restaurantsFile);
        FoodItems = FileHelper.ReadFromFile<FoodItem>(foodItemsFile);
        Users = FileHelper.ReadFromFile<User>(usersFile);

        if (Users == null || Users.Count == 0)
        {
            Users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "superadmin",
                    Email = "super@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Role = "SuperAdmin"
                }
            };

            FileHelper.WriteToFile(usersFile, Users);
        }
    }

    public static void SaveUsers()
    {
        FileHelper.WriteToFile(usersFile, Users);
    }

    public static void SaveRestaurants()
    {
        FileHelper.WriteToFile(restaurantsFile, Restaurants);
    }

    public static void SaveFoodItems()
    {
        FileHelper.WriteToFile(foodItemsFile, FoodItems);
    }

    static partial void InitializeRestaurants();
    static partial void InitializeFoodItems();
}