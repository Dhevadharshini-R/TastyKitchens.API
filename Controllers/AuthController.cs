using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.Services;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Models;

namespace TastyKitchens.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

            var user = _authService.Register(dto);
            if (user == null) 
                return BadRequest(ApiResponse<object>.FailureResponse("User already exists"));

            return Created("", ApiResponse<User>.SuccessResponse(user, "User registered successfully"));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.FailureResponse("Invalid input data"));

            var user = _authService.Login(dto);
            if (user == null) 
                return Unauthorized(ApiResponse<object>.FailureResponse("Invalid email or password"));

            var token = _jwtService.GenerateToken(user);

            return Ok(ApiResponse<object>.SuccessResponse(new { token }, "Login successful"));
        }
    }
}