using Ecom.Core.DTO;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("get-orders-for-user")]
    public async Task<IActionResult> GetOrders()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized();
        }
        var orders = await _orderService.GetAllOrdersForUserAsync(email);
        return Ok(orders);
    }

    [HttpGet("get-order-by-id/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized();
        }
        var order = await _orderService.GetOrderByIdAsync(id, email);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("get-delivery")]
    public async Task<IActionResult> GetDeliveryMethods()
    => Ok(await _orderService.GetDeliveryMethodsAsync());


    [HttpPost("create-order")]
    public async Task<IActionResult> Create(OrderDTO orderDTO)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized();
        }
        var order = await _orderService.CreateOrderAsync(orderDTO,email);
        return Ok(order);
    }

}
