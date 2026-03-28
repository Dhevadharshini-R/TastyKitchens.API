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
        if (!FakeDb.Restaurants.Any(r => r.Id == dto.RestaurantId))
            return null;

        var nextId = FakeDb.FoodItems.Any() ? FakeDb.FoodItems.Max(f => f.Id) + 1 : 1;
        var foodItem = new FoodItem
        {
            Id = nextId,
            RestaurantId = dto.RestaurantId,
            Name = dto.Name,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            Rating = dto.Rating
        };

        FakeDb.FoodItems.Add(foodItem);
        FakeDb.SaveFoodItemsToFile(); 
        return foodItem;
    }

    public FoodItem UpdateFoodItem(int id, UpdateFoodItemDto dto)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        existing.Name = dto.Name;
        existing.Price = dto.Price;
        existing.ImageUrl = dto.ImageUrl;
        if (dto.Rating > 0) existing.Rating = dto.Rating;

        FakeDb.SaveFoodItemsToFile();
        return existing;
    }

    public bool DeleteFoodItem(int id)
    {
        var foodItem = GetById(id);
        if (foodItem == null) return false;

        FakeDb.FoodItems.Remove(foodItem);
        FakeDb.SaveFoodItemsToFile();
        return true;
    }
}
