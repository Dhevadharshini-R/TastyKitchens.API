using TastyKitchens.API.Models;
using BCrypt.Net;

namespace TastyKitchens.API.Data;

public static partial class FakeDb
{
    public static List<Restaurant> Restaurants = new List<Restaurant>();
    public static List<FoodItem> FoodItems = new List<FoodItem>();
    public static List<User> Users = new List<User>();

    static FakeDb()
    {
        InitializeRestaurants();
        InitializeFoodItems();
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
    }

    static partial void InitializeRestaurants();
    static partial void InitializeFoodItems();
}
