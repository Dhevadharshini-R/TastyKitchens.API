using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/fooditems")]
[Authorize]
public class FoodItemsController : ControllerBase
{
    private readonly FoodItemService _service = new FoodItemService();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetFoodItems() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetFoodItem(int id)
    {
        var foodItem = _service.GetById(id);
        if (foodItem == null) return NotFound();
        return Ok(foodItem);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateFoodItem([FromBody] CreateFoodItemDto dto)
    {
        var foodItem = _service.AddFoodItem(dto);
        if (foodItem == null)
            return BadRequest("Restaurant not found. Food items must belong to an existing restaurant.");

        return CreatedAtAction(nameof(GetFoodItem), new { id = foodItem.Id }, foodItem);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateFoodItem(int id, [FromBody] UpdateFoodItemDto dto)
    {
        var foodItem = _service.UpdateFoodItem(id, dto);
        if (foodItem == null) return NotFound();
        return Ok(foodItem);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult DeleteFoodItem(int id)
    {
        var success = _service.DeleteFoodItem(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
