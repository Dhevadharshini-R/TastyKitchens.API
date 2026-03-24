using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Helpers;

namespace TastyKitchens.API.Services;

public class RestaurantService
{
    private readonly string path;
    private readonly string foodPath;

    public RestaurantService()
    {
        var basePath = Directory.GetCurrentDirectory();
        path = Path.Combine(basePath, "Data", "restaurants.json");
        foodPath = Path.Combine(basePath, "Data", "fooditems.json");
    }

    // ✅ GET ALL
    public List<Restaurant> GetAll()
    {
        return FileHelper.ReadFromFile<Restaurant>(path);
    }

    // ✅ GET BY ID
    public Restaurant GetById(int id)
    {
        var restaurants = FileHelper.ReadFromFile<Restaurant>(path);
        return restaurants.FirstOrDefault(r => r.Id == id);
    }

    // ✅ GET FOOD ITEMS
    public List<FoodItem> GetFoodItemsByRestaurant(int restaurantId)
    {
        var foods = FileHelper.ReadFromFile<FoodItem>(foodPath);
        return foods.Where(f => f.RestaurantId == restaurantId.ToString()).ToList();
    }

    // ✅ CREATE
    public Restaurant AddRestaurant(CreateRestaurantDto dto)
    {
        var restaurants = FileHelper.ReadFromFile<Restaurant>(path);

        var newId = restaurants.Any() ? restaurants.Max(r => r.Id) + 1 : 1;

        var restaurant = new Restaurant
        {
            Id = newId,
            Name = dto.Name,
            ImageUrl = dto.ImageUrl,
            Cuisine = dto.Cuisine,
            Location = dto.Location,
            CostForTwo = dto.CostForTwo,
            DeliveryTime = "30 mins",
            Distance = "2.5 km",
            IsOpen = true,
            Rating = 0,
            TotalReviews = 0
        };

        restaurants.Add(restaurant);
        FileHelper.WriteToFile(path, restaurants);

        return restaurant;
    }

    // ✅ UPDATE
    public Restaurant UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        var restaurants = FileHelper.ReadFromFile<Restaurant>(path);

        var existing = restaurants.FirstOrDefault(r => r.Id == id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.ImageUrl = dto.ImageUrl;
        existing.Cuisine = dto.Cuisine;
        existing.Location = dto.Location;
        existing.CostForTwo = dto.CostForTwo;

        FileHelper.WriteToFile(path, restaurants);

        return existing;
    }

    // ✅ DELETE
    public bool DeleteRestaurant(int id)
    {
        var restaurants = FileHelper.ReadFromFile<Restaurant>(path);

        var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
        if (restaurant == null) return false;

        restaurants.Remove(restaurant);
        FileHelper.WriteToFile(path, restaurants);

        return true;
    }
}