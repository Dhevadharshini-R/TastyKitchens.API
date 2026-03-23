using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/fooditems")]
[Authorize]
public class FoodItemsController : ControllerBase
{
    private readonly FoodItemService _service = new FoodItemService();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetFoodItems() 
    {
        var foodItems = _service.GetAll();
        return Ok(ApiResponse<IEnumerable<FoodItem>>.SuccessResponse(foodItems));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetFoodItem(int id)
    {
        var foodItem = _service.GetById(id);
        if (foodItem == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Food item not found"));

        return Ok(ApiResponse<FoodItem>.SuccessResponse(foodItem));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateFoodItem([FromBody] CreateFoodItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var foodItem = _service.AddFoodItem(dto);
        if (foodItem == null)
            return BadRequest(ApiResponse<object>.FailureResponse("Restaurant not found. Food items must belong to an existing restaurant."));

        return CreatedAtAction(nameof(GetFoodItem), new { id = foodItem.Id }, ApiResponse<FoodItem>.SuccessResponse(foodItem, "Food item created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateFoodItem(int id, [FromBody] UpdateFoodItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var foodItem = _service.UpdateFoodItem(id, dto);
        if (foodItem == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Food item not found"));

        return Ok(ApiResponse<FoodItem>.SuccessResponse(foodItem, "Food item updated successfully"));
    }

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
