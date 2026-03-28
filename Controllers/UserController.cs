using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;

namespace TastyKitchens.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService = new UserService();

    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var user = _userService.GetUserByEmail(email);

        return Ok(user);
    }

    [HttpPut("profile")]
    public IActionResult UpdateProfile([FromBody] UpdateUserProfileDto dto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var user = _userService.UpdateProfile(email, dto.PhoneNumber, dto.Address);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }
}