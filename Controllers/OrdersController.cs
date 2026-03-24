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

    // ✅ PLACE ORDER
    [HttpPost]
    public IActionResult PlaceOrder(CreateOrderRequest request)
    {
        try
        {
            // 🔥 TAKE USER FROM TOKEN (NO MANUAL USERID)
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            request.UserId = email;

            var order = _orderService.PlaceOrder(request);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ USER → ONLY THEIR ORDERS
    [HttpGet("my")]
    public IActionResult GetMyOrders()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        var orders = _orderService.GetUserOrders(email);
        return Ok(orders);
    }

    // ✅ ADMIN → ALL ORDERS
    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet("all")]
    public IActionResult GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();
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