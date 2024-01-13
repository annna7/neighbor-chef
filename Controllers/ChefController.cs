using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using FirebaseAdmin.Messaging;
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
public class ChefController : ControllerBase
{
   private readonly IChefService _chefService;
   private readonly IOrderService _orderService;
   private readonly IAccountService _accountService;
   
   public ChefController(IChefService chefService, IOrderService orderService, IAccountService accountService)
   {
      _chefService = chefService;
      _orderService = orderService;
      _accountService = accountService;
   }
   
   [Authorize(AuthenticationSchemes = "Bearer")]
   [HttpGet("all")]
   public async Task<IActionResult> GetAllChefs()
   {
      var chefList = await _chefService.GetAllChefsAsync();
      return Ok(chefList);
   }
   
   [Authorize(AuthenticationSchemes = "Bearer")]
   [HttpGet("")]
   public async Task<IActionResult> GetChef()
   {
      var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
      var chef = await _chefService.GetChefAsync(chefEmail);
      if (chef == null)
      {
         return NotFound("Chef with email " + chefEmail + " not found");
      }
      
      return Ok(chef);
   }
   
   [Authorize(AuthenticationSchemes = "Bearer")]
   [HttpGet("{chefId:guid}")]
   public async Task<IActionResult> GetChef(Guid chefId)
   {
      var chef = await _chefService.GetChefAsync(chefId);
      if (chef == null)
      {
        return NotFound("Chef with id " + chefId + " not found");
      }
      return Ok(chef);
   }

   
   [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
   [HttpPut("orders/{orderId:guid}")]
   public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusDto orderStatusDto)
   {
      var chefEmail = _accountService.GetEmailFromToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
      var chef = await _chefService.GetPersonAsync(chefEmail);
      if (chef == null)
      {
         return NotFound("chef with email " + chefEmail + " not found");
      }
      var order = await _orderService.GetOrderByIdAsync(orderId);
      if (order.ChefId != chef.Id)
      {
         return BadRequest("Chef with email " + chefEmail + " is not the owner of order with id " + orderId);
      }

      try
      {
         await _orderService.UpdateOrderStatusAsync(orderId, orderStatusDto.Status, true);
      }
      catch (InvalidOrderStatusTransitionException e)
      {
         return BadRequest(e.Message);
      }
      return NoContent();
   }
  
   [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
   [IsSame("chefId")]
   [HttpPost("{chefId}/dates")]
   public async Task<IActionResult> AddAvailableDate(Guid chefId, [FromBody] string date)
   {
      try
      {
         Console.WriteLine("Adding date" + date);
         var result = await _chefService.AddAvailableDateAsync(chefId, date);
         return Ok(result);
      }
      catch (ChefNotFoundException e)
      {
         return NotFound(e.Message);
      }
      catch (DateAlreadyAvailableException e)
      {
         return BadRequest(e.Message);
      }
      catch (DateCantBeAvailableException e)
      {
         return BadRequest(e.Message);
      }
   }
   
   
   [HttpGet("{chefId}/dates")]
   public async Task<IActionResult> GetAvailableDates(Guid chefId)
   {
      if (await _chefService.GetPersonAsync(chefId) is not Chef chef)
      {
         return NotFound("Chef not found.");
      }
      return Ok(chef.AvailableDates);
   }

   [Authorize(Roles = "Chef", AuthenticationSchemes = "Bearer")]
   [IsSame("chefId")]
   [HttpDelete("{chefId:guid}/dates/{date}")]
   public async Task<IActionResult> DeleteAvailableDate(Guid chefId, string date)
   {
      try
      {
         await _chefService.RemoveAvailableDateAsync(chefId, date);
      }
      catch (DateNotFoundException e)
      {
         return NotFound(e.Message);
      }
      catch (ChefNotFoundException e) {
         return NotFound(e.Message);
      }

      return Ok();
   }
}