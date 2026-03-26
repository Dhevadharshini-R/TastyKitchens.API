using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Data;

namespace TastyKitchens.API.Services;

public class RestaurantService
{
    // GET ALL
    public List<Restaurant> GetAll()
    {
        return FakeDb.Restaurants;
    }

    // GET BY ID
    public Restaurant GetById(int id)
    {
        return FakeDb.Restaurants.FirstOrDefault(r => r.Id == id);
    }

    // GET FOOD ITEMS
    public List<FoodItem> GetFoodItemsByRestaurant(int restaurantId)
    {
        return FakeDb.FoodItems.Where(f => f.RestaurantId == restaurantId.ToString()).ToList();
    }

    // CREATE
    public Restaurant AddRestaurant(CreateRestaurantDto dto)
    {
        var newId = FakeDb.Restaurants.Any() ? FakeDb.Restaurants.Max(r => r.Id) + 1 : 1;

        var restaurant = new Restaurant
        {
            Id = newId,
            Name = dto.Name,
            ImageUrl = dto.ImageUrl,
            Cuisine = dto.Cuisine,
            Location = dto.Location,
            CostForTwo = dto.CostForTwo,
            DeliveryTime = dto.DeliveryTime ?? "30 mins",
            Distance = dto.Distance ?? "2.5 km",
            IsOpen = dto.IsOpen,
            Rating = dto.Rating,
            TotalReviews = dto.TotalReviews
        };

        FakeDb.Restaurants.Add(restaurant);
        FakeDb.SaveRestaurantsToFile();
        return restaurant;
    }

    // UPDATE
    public Restaurant UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        var existing = FakeDb.Restaurants.FirstOrDefault(r => r.Id == id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.ImageUrl = dto.ImageUrl;
        existing.Cuisine = dto.Cuisine;
        existing.Location = dto.Location;
        existing.CostForTwo = dto.CostForTwo;
        if (dto.DeliveryTime != null) existing.DeliveryTime = dto.DeliveryTime;
        if (dto.Distance != null) existing.Distance = dto.Distance;
        existing.IsOpen = dto.IsOpen;
        if (dto.Rating > 0) existing.Rating = dto.Rating;
        if (dto.TotalReviews > 0) existing.TotalReviews = dto.TotalReviews;

        FakeDb.SaveRestaurantsToFile();
        return existing;
    }

    // DELETE
    public bool DeleteRestaurant(int id)
    {
        var restaurant = FakeDb.Restaurants.FirstOrDefault(r => r.Id == id);
        if (restaurant == null) return false;

        FakeDb.Restaurants.Remove(restaurant);
        FakeDb.SaveRestaurantsToFile();
        return true;
    }
}