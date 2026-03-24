using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace TastyKitchens.API.Controllers;

[Authorize] // 🔥 ADD THIS (controller level)
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController()
    {
        _orderService = new OrderService();
    }

    [HttpPost]
    public IActionResult PlaceOrder(CreateOrderRequest request)
    {
        try
        {
            var order = _orderService.PlaceOrder(request);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserOrders(string userId)
    {
        var orders = _orderService.GetUserOrders(userId);
        return Ok(orders);
    }

    [Authorize(Roles = "Admin,SuperAdmin")] // 🔥 already correct
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