using Microsoft.AspNetCore.Mvc;
using TastyKitchens.API.DTOs;
using TastyKitchens.API.Services;

namespace TastyKitchens.API.Controllers;

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