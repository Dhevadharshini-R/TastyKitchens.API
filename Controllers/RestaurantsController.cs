using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _service = new RestaurantService();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetRestaurants() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetRestaurant(int id)
    {
        var restaurant = _service.GetById(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    [HttpGet("{id}/fooditems")]
    [AllowAnonymous]
    public IActionResult GetFoodItems(int id) => Ok(_service.GetFoodItemsByRestaurant(id));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var restaurant = _service.AddRestaurant(dto);
        return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto dto)
    {
        var restaurant = _service.UpdateRestaurant(id, dto);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult DeleteRestaurant(int id)
    {
        var success = _service.DeleteRestaurant(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
