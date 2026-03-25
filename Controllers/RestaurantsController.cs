using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;

// 🔥 ADDED
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

    // ✅ GET ALL
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var restaurant = _service.GetById(id);

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }

    // ✅ GET FOOD ITEMS OF RESTAURANT
    [HttpGet("{id}/fooditems")]
    public IActionResult GetFoodItems(int id)
    {
        return Ok(_service.GetFoodItemsByRestaurant(id));
    }

    // ✅ CREATE
    // 🔥 ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public IActionResult Create(CreateRestaurantDto dto)
    {
        var restaurant = _service.AddRestaurant(dto);

        // 🔥 ADDED: SET ADMIN EMAIL FROM TOKEN
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        restaurant.AdminEmail = email;

        return Ok(restaurant);
    }

    // ✅ UPDATE
    // 🔥 ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = _service.UpdateRestaurant(id, dto);

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }

    // ✅ DELETE
    // 🔥 ADDED AUTHORIZATION
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _service.DeleteRestaurant(id);

        if (!success)
            return NotFound();

        return Ok("Deleted successfully");
    }
}