using Microsoft.AspNetCore.Mvc;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
    // Upload Restaurant Image
    [HttpPost("restaurant")]
    public async Task<IActionResult> UploadRestaurant(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/restaurants");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"/images/restaurants/{fileName}";
        return Ok(new { imageUrl = url });
    }

    // Upload Food Image
    [HttpPost("fooditem")]
    public async Task<IActionResult> UploadFood(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/food");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var url = $"/images/food/{fileName}";
        return Ok(new { imageUrl = url });
    }
}