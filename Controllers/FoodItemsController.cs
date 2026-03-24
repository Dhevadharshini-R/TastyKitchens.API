using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;

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

    // ✅ GET ALL
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var item = _service.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // ✅ CREATE
    [HttpPost]
    public IActionResult Create(CreateFoodItemDto dto)
    {
        var item = _service.Create(dto);
        return Ok(item);
    }

    // ✅ UPDATE
    [HttpPut("{id}")]
    public IActionResult Update(string id, UpdateFoodItemDto dto)
    {
        var item = _service.Update(id, dto);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var success = _service.Delete(id);

        if (!success)
            return NotFound();

        return Ok("Deleted successfully");
    }
}