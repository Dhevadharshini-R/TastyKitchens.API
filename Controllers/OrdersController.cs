using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TastyKitchens.API.Controllers;

[Authorize]
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController()
    {
        _orderService = new OrderService();
    }

    // ✅ PLACE ORDER (USER ONLY)
    
    [HttpPost]
    [Authorize(Roles = "User")]
    public IActionResult PlaceOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var order = _orderService.PlaceOrder(request, userEmail);

            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ GET MY ORDERS (USER)
    [HttpGet("my")]
    [Authorize(Roles = "User")]
    public IActionResult GetMyOrders()
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        var orders = _orderService.GetMyOrders(userEmail);

        return Ok(orders);
    }

    // ✅ GET ALL ORDERS (ADMIN ONLY)
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet("all")]
    public IActionResult GetAllOrders()
    {
         var email = User.FindFirst(ClaimTypes.Email)?.Value;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;

    var orders = _orderService.GetAllOrders(email, role);

    return Ok(orders);
    }

    // ✅ UPDATE STATUS (ADMIN ONLY)
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("{orderId}/status")]
    public IActionResult UpdateStatus(string orderId, [FromQuery] string status)
    {
        try
        {
            var updatedOrder = _orderService.UpdateOrderStatus(orderId, status);
            return Ok(updatedOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}