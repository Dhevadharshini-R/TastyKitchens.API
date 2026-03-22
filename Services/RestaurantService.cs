using TastyKitchens.API.Data;
using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Services;

public class RestaurantService
{
    public List<Restaurant> GetAll() => FakeDb.Restaurants;

    public Restaurant GetById(int id) => FakeDb.Restaurants.FirstOrDefault(r => r.Id == id);

    public List<FoodItem> GetFoodItemsByRestaurant(int restaurantId) =>
        FakeDb.FoodItems.Where(f => f.RestaurantId == restaurantId).ToList();

    public Restaurant AddRestaurant(CreateRestaurantDto dto)
    {
        var nextId = FakeDb.Restaurants.Any() ? FakeDb.Restaurants.Max(r => r.Id) + 1 : 1;
        var restaurant = new Restaurant
        {
            Id = nextId,
            Name = dto.Name,
            ImageUrl = dto.ImageUrl,
            Cuisine = dto.Cuisine,
            Location = dto.Location,
            CostForTwo = dto.CostForTwo,
            DeliveryTime = "30 mins", // Default for demo
            Distance = "2.5 km",      // Default for demo
            IsOpen = true,
            Rating = 0,
            TotalReviews = 0
        };
        FakeDb.Restaurants.Add(restaurant);
        return restaurant;
    }

    public Restaurant UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.ImageUrl = dto.ImageUrl;
        existing.Cuisine = dto.Cuisine;
        existing.Location = dto.Location;
        existing.CostForTwo = dto.CostForTwo;

        return existing;
    }

    public bool DeleteRestaurant(int id)
    {
        var restaurant = GetById(id);
        if (restaurant == null) return false;

        FakeDb.Restaurants.Remove(restaurant);
        return true;
    }
}
