using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Services;

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

   [HttpGet]
   [Authorize(AuthenticationSchemes = "Bearer")]
   public async Task<IActionResult> GetChefs()
   {
      var chefs = await _chefService.GetChefsSortedAsync();
      return Ok(chefs);
   }
}