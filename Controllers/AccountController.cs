using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models;
using neighbor_chef.Models.DTOs;
using neighbor_chef.Services;
using neighbor_chef.UnitOfWork;
using neighbor_chef.TokenService;

namespace neighbor_chef.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IChefService _chefService;
    private readonly ICustomerService _customerService;

    public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IChefService chefService, ICustomerService customerService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _chefService = chefService;
        _customerService = customerService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] JsonElement person)
    {
        var typeDiscriminator = person.GetProperty("Type").GetString();
        PersonRegisterDto personDto;

        switch (typeDiscriminator)
        {
            case "Chef":
                var chefDto = JsonSerializer.Deserialize<ChefRegisterDto>(person.GetRawText());
                var chef = await _chefService.CreatePersonAsync(chefDto);
                return Ok(chef);
            case "Customer":
                var customerDto = JsonSerializer.Deserialize<CustomerRegisterDto>(person.GetRawText());
                var customer = await _customerService.CreatePersonAsync(customerDto);
                return Ok(customer);
            default:
                return BadRequest("Invalid person type.");
        }
    }
    
    [HttpPost("register-chef")]
    public async Task<IActionResult> RegisterChef(ChefRegisterDto model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
    
        var chef = new Chef
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            ApplicationUserId = user.Id,
            AddressId = new Guid("709112f5-8b25-4fb1-6c5a-08dbe38d3ff2"),
            Description = model.Description,
            AdvanceNoticeDays = model.AdvanceNoticeDays,
            MaxOrdersPerDay = model.MaxOrdersPerDay,
        };
        
        await _userManager.AddToRoleAsync(user, "Chef");
        
        var chefRepository = _unitOfWork.GetRepository<Chef>();
        await chefRepository.AddAsync(chef);
        await _unitOfWork.CompleteAsync();
        
        return Ok(new { Username = user.UserName, Email = user.Email });

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null) return Unauthorized("User not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles); // Your method to create a JWT token
            return Ok(new { Token = token });
        }

        return Unauthorized("Invalid credentials.");
    }
}
