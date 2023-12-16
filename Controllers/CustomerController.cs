using System.Security.Claims;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Exceptions.Dates;
using neighbor_chef.Exceptions.Orders;
using neighbor_chef.Exceptions.People;
using neighbor_chef.Models;
using neighbor_chef.Services;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Orders;
using neighbor_chef.Services.Orders;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
   private readonly ICustomerService _customerService;
   private readonly IOrderService _orderService;
   
   public CustomerController(ICustomerService customerService, IOrderService orderService)
   {
      _customerService = customerService;
      _orderService = orderService;
   }
   
   [HttpGet("{customerId:guid}")]
   public async Task<IActionResult> GetCustomer(Guid customerId)
   {
     var customer = await _customerService.GetCustomerAsync(customerId);
     if (customer == null)
     {
       return NotFound("Customer with id " + customerId + " not found");
     }
     return Ok(customer);
   }
   
   [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
   [HttpPut("orders/{orderId:guid}")]
   public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusDto orderStatusDto)
   {
       var customerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
       if (customerEmail == null)
       {
           return BadRequest("Invalid customer email");
       }
       var customer = await _customerService.GetPersonAsync(customerEmail);
       if (customer == null)
       {
           return NotFound("Customer with email " + customerEmail + " not found");
       }
       var order = await _orderService.GetOrderByIdAsync(orderId);
       if (order.CustomerId != customer.Id)
       {
           return BadRequest("Customer with email " + customerEmail + " is not the owner of order with id " + orderId);
       }

       try
       {
           await _orderService.UpdateOrderStatusAsync(orderId, orderStatusDto.Status, false);
       }
       catch (InvalidOrderStatusTransitionException e)
       {
           return BadRequest(e.Message);
       }
       return NoContent();
   }
}