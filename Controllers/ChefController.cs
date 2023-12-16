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
public class ChefController : ControllerBase
{
   private readonly IChefService _chefService;
   
   public ChefController(IChefService chefService)
   {
      _chefService = chefService;
   }
   
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
   [IsSame("chefId")]
   [HttpPost("{chefId}/dates")]
   public async Task<IActionResult> AddAvailableDate(Guid chefId, [FromBody] DateDto date)
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
   [HttpDelete("{chefId}/dates")]
   public async Task<IActionResult> DeleteAvailableDate(Guid chefId, DateDto date)
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