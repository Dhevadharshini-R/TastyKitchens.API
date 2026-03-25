using TastyKitchens.API.Models;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Data;

namespace TastyKitchens.API.Services;

public class FoodItemService
{
    // GET ALL
    public List<FoodItem> GetAll()
    {
        return FakeDb.FoodItems;
    }

    // GET BY ID
    public FoodItem GetById(string id)
    {
        return FakeDb.FoodItems.FirstOrDefault(f => f.Id == id);
    }

    // CREATE
    public FoodItem Create(CreateFoodItemDto dto)
    {
        var newId = "f" + (FakeDb.FoodItems.Count + 1);

        var foodItem = new FoodItem
        {
            Id = newId,
            RestaurantId = dto.RestaurantId,
            Name = dto.Name,
            Price = dto.Price,
            Rating = 0,
            ImageUrl = dto.ImageUrl
        };

        FakeDb.FoodItems.Add(foodItem);
        FakeDb.SaveFoodItems(); // SAVE

        return foodItem;
    }

    // UPDATE
    public FoodItem Update(string id, UpdateFoodItemDto dto)
    {
        var existing = FakeDb.FoodItems.FirstOrDefault(f => f.Id == id);

        if (existing == null)
            return null;

        existing.Name = dto.Name;
        existing.Price = dto.Price;
        existing.ImageUrl = dto.ImageUrl;

        FakeDb.SaveFoodItems(); // SAVE

        return existing;
    }

    // DELETE
    public bool Delete(string id)
    {
        var food = FakeDb.FoodItems.FirstOrDefault(f => f.Id == id);

        if (food == null)
            return false;

        FakeDb.FoodItems.Remove(food);
        FakeDb.SaveFoodItems(); // SAVE

        return true;
    }
}