using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Models;

// ADDED
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _service;

    public RestaurantsController()
    {
        _service = new RestaurantService();
    }

    // GET ALL
    [HttpGet]
<<<<<<< HEAD
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
=======
    [AllowAnonymous]
    public IActionResult GetRestaurants() 
    {
        var restaurants = _service.GetAll();
        return Ok(ApiResponse<IEnumerable<Restaurant>>.SuccessResponse(restaurants));
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    }

    // GET BY ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var restaurant = _service.GetById(id);
<<<<<<< HEAD

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
=======
        if (restaurant == null) 
            return NotFound(ApiResponse<object>.FailureResponse("Restaurant not found"));
        
        return Ok(ApiResponse<Restaurant>.SuccessResponse(restaurant));
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    }

    // GET FOOD ITEMS OF RESTAURANT
    [HttpGet("{id}/fooditems")]
<<<<<<< HEAD
    public IActionResult GetFoodItems(int id)
    {
        return Ok(_service.GetFoodItemsByRestaurant(id));
=======
    [AllowAnonymous]
    public IActionResult GetFoodItems(int id) 
    {
        var foodItems = _service.GetFoodItemsByRestaurant(id);
        return Ok(ApiResponse<IEnumerable<FoodItem>>.SuccessResponse(foodItems));
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    }

    // CREATE
    // ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
<<<<<<< HEAD
    public IActionResult Create(CreateRestaurantDto dto)
=======
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

        var restaurant = _service.AddRestaurant(dto);
<<<<<<< HEAD

        // ADDED: SET ADMIN EMAIL FROM TOKEN
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        restaurant.AdminEmail = email;

        return Ok(restaurant);
=======
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
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    }

    // UPDATE
    // ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = _service.UpdateRestaurant(id, dto);

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }

    // DELETE
    // ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _service.DeleteRestaurant(id);
<<<<<<< HEAD

        if (!success)
            return NotFound();

        return Ok("Deleted successfully");
=======
        if (!success) 
            return NotFound(ApiResponse<object>.FailureResponse("Restaurant not found"));

        return Ok(ApiResponse<object>.SuccessResponse(null, "Restaurant deleted successfully"));
>>>>>>> 07942cedc5074d22fe08a26d35e1bcc75de64bb7
    }
}