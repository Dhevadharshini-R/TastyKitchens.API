using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/fooditems")]
public class FoodItemsController : ControllerBase
{
    private readonly FoodItemService _service;

    public FoodItemsController()
    {
        _service = new FoodItemService();
    }

    // GET ALL
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetFoodItems() 
    {
        var foodItems = _service.GetAll();
        return Ok(ApiResponse<IEnumerable<FoodItem>>.SuccessResponse(foodItems));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var foodItem = _service.GetById(id);
        if (foodItem == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Food item not found"));

        return Ok(ApiResponse<FoodItem>.SuccessResponse(foodItem));
    }

    // CREATE
    // ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public IActionResult Create(CreateFoodItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var foodItem = _service.AddFoodItem(dto);
        if (foodItem == null)
            return BadRequest(ApiResponse<object>.FailureResponse("Restaurant not found. Food items must belong to an existing restaurant."));

        return CreatedAtAction(nameof(GetFoodItem), new { id = foodItem.Id }, ApiResponse<FoodItem>.SuccessResponse(foodItem, "Food item created successfully"));
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, UpdateFoodItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var foodItem = _service.UpdateFoodItem(id, dto);
        if (foodItem == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Food item not found"));

        return Ok(ApiResponse<FoodItem>.SuccessResponse(foodItem, "Food item updated successfully"));
    }

    // DELETE
    // ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteFoodItem(int id)
    {
        var success = _service.DeleteFoodItem(id);
        if (!success) 
            return NotFound(ApiResponse<object>.FailureResponse("Food item not found"));

        return Ok(ApiResponse<object>.SuccessResponse(null, "Food item deleted successfully"));
    }
}