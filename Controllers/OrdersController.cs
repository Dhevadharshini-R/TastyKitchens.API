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
    [Authorize(Roles = "User")]
    [HttpPost]
    public IActionResult PlaceOrder(CreateOrderRequest request)
    {
        try
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var order = _orderService.PlaceOrder(request, email); // 🔥 CHANGE

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

    // ✅ ADMIN / SUPERADMIN
    [HttpGet("all")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public IActionResult GetAllOrders()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (role == "SuperAdmin")
        {
            return Ok(_orderService.GetAllOrders());
        }

        var orders = _orderService.GetOrdersByAdmin(email);
        return Ok(orders);
    }

    // ✅ UPDATE STATUS
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