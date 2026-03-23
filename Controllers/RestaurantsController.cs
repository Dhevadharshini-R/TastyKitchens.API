using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _service = new RestaurantService();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetRestaurants() 
    {
        var restaurants = _service.GetAll();
        return Ok(ApiResponse<IEnumerable<Restaurant>>.SuccessResponse(restaurants));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetRestaurant(int id)
    {
        var restaurant = _service.GetById(id);
        if (restaurant == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Restaurant not found"));
        
        return Ok(ApiResponse<Restaurant>.SuccessResponse(restaurant));
    }

    [HttpGet("{id}/fooditems")]
    [AllowAnonymous]
    public IActionResult GetFoodItems(int id) 
    {
        var foodItems = _service.GetFoodItemsByRestaurant(id);
        return Ok(ApiResponse<IEnumerable<FoodItem>>.SuccessResponse(foodItems));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var restaurant = _service.AddRestaurant(dto);
        return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, ApiResponse<Restaurant>.SuccessResponse(restaurant, "Restaurant created successfully"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var restaurant = _service.UpdateRestaurant(id, dto);
        if (restaurant == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Restaurant not found"));

        return Ok(ApiResponse<Restaurant>.SuccessResponse(restaurant, "Restaurant updated successfully"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult DeleteRestaurant(int id)
    {
        var success = _service.DeleteRestaurant(id);
        if (!success) 
            return NotFound(ApiResponse<object>.FailureResponse("Restaurant not found"));

        return Ok(ApiResponse<object>.SuccessResponse(null, "Restaurant deleted successfully"));
    }
}
