using TastyKitchens.API.Data;
using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Services;

public class FoodItemService
{
    public List<FoodItem> GetAll() => FakeDb.FoodItems;

    public FoodItem GetById(int id) => FakeDb.FoodItems.FirstOrDefault(f => f.Id == id);

    public FoodItem AddFoodItem(CreateFoodItemDto dto)
    {
        // ✅ Validate Restaurant exists before adding food
        if (!FakeDb.Restaurants.Any(r => r.Id == dto.RestaurantId))
            return null;

        var nextId = FakeDb.FoodItems.Any() ? FakeDb.FoodItems.Max(f => f.Id) + 1 : 1;
        var foodItem = new FoodItem
        {
            Id = nextId,
            RestaurantId = dto.RestaurantId,
            Name = dto.Name,
            Cost = dto.Cost,
            ImageUrl = dto.ImageUrl,
            Rating = 0 // Default for new items
        };
        FakeDb.FoodItems.Add(foodItem);
        return foodItem;
    }

    public FoodItem UpdateFoodItem(int id, UpdateFoodItemDto dto)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.Cost = dto.Cost;
        existing.ImageUrl = dto.ImageUrl;

        return existing;
    }

    public bool DeleteFoodItem(int id)
    {
        var foodItem = GetById(id);
        if (foodItem == null) return false;

        FakeDb.FoodItems.Remove(foodItem);
        return true;
    }
}
