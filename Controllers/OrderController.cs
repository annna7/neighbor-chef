using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models.DTOs.Orders;
using neighbor_chef.Services;
using neighbor_chef.Services.Orders;

namespace neighbor_chef.Controllers;

[ApiController]
[Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IChefService _chefService;
    private readonly ICustomerService _customerService;

    public OrderController(IOrderService orderService, IChefService chefService, ICustomerService customerService)
    {
        _orderService = orderService;
        _chefService = chefService;
        _customerService = customerService;
    }

    [HttpPost("{chefId:guid}")]
    public async Task<IActionResult> CreateOrder(Guid chefId, [FromBody] CreateOrderDto orderDto)
    {
        var customerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (customerEmail == null) return Unauthorized("You are not authorized to create an order, please log in as a customer.");
        var customer = await _customerService.GetPersonAsync(customerEmail, true);
        if (customer == null) return Unauthorized("You are not authorized to create an order, please log in as a customer.");
        var order = await _orderService.CreateOrderAsync(customer.Id, chefId, orderDto);
        return Ok(order);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        return Ok(order);
    }

    [HttpPut("{orderId:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderDto orderDto)
    {
        await _orderService.UpdateOrderAsync(orderId, orderDto);
        return NoContent();
    }

    [HttpDelete("{customerId:guid}/{orderId:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid customerId, Guid orderId)
    {
        await _orderService.DeleteOrderAsync(customerId, orderId);
        return NoContent();
    }
}