using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Exceptions.Dates;
using neighbor_chef.Exceptions.People;
using neighbor_chef.Models;
using neighbor_chef.Services;
using neighbor_chef.Models.DTOs;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
   private readonly ICustomerService _customerService;
   
   public CustomerController(ICustomerService customerService)
   {
      _customerService = customerService;
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
}