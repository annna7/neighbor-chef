using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Models.DTOs.Authentication;
using neighbor_chef.Services;
using neighbor_chef.TokenService;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IChefService _chefService;
    private readonly ICustomerService _customerService;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IChefService chefService, ICustomerService customerService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _chefService = chefService;
        _customerService = customerService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        var person = await JsonSerializer.DeserializeAsync<JsonElement>(Request.Body);
        if (!person.TryGetProperty("Type", out var typeDiscriminatorElement))
        {
            return BadRequest("Invalid Request!");
        }
        var typeDiscriminator = typeDiscriminatorElement.GetString();

        switch (typeDiscriminator)
        {
            case "Chef":
                var chefDto = JsonSerializer.Deserialize<ChefRegisterDto>(person.GetRawText());
                if (chefDto == null) return BadRequest("Invalid chef DTO.");
                var chef = await _chefService.CreatePersonAsync(chefDto);
                return Ok(chef);
            case "Customer":
                var customerDto = JsonSerializer.Deserialize<CustomerRegisterDto>(person.GetRawText());
                if (customerDto == null) return BadRequest("Invalid customer DTO.");
                await Console.Error.WriteLineAsync(customerDto.ToString());
                var customer = await _customerService.CreatePersonAsync(customerDto);
                return Ok(customer);
            default:
                return BadRequest("Invalid person type.");
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) return Unauthorized("User not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded) return Unauthorized("Invalid credentials.");
        
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);
        return Ok(new { Token = token });
    }
}
