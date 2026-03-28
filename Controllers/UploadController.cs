using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/upload")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public UploadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("restaurant")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 400)]
    public async Task<IActionResult> UploadRestaurantImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse<object>.FailureResponse("No file uploaded"));

        var folderPath = Path.Combine(_env.WebRootPath, "images", "restaurants");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"{Request.Scheme}://{Request.Host}/images/restaurants/{fileName}";

        return Ok(ApiResponse<object>.SuccessResponse(new { imageUrl }, "Restaurant image uploaded successfully"));
    }

    [HttpPost("fooditem")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), 200)]
    [ProducesResponseType(typeof(ApiResponse<object>), 400)]
    public async Task<IActionResult> UploadFoodItemImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse<object>.FailureResponse("No file uploaded"));

        var folderPath = Path.Combine(_env.WebRootPath, "images", "fooditems");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"{Request.Scheme}://{Request.Host}/images/fooditems/{fileName}";

        return Ok(ApiResponse<object>.SuccessResponse(new { imageUrl }, "Food item image uploaded successfully"));
    }
}
