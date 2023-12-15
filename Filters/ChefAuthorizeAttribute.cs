using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using neighbor_chef.Services;

namespace neighbor_chef.Filters;

public class ChefAuthorizeAttribute : ActionFilterAttribute
{
    private readonly IChefService _chefService;

    public ChefAuthorizeAttribute(IChefService chefService)
    {
        _chefService = chefService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        var customerEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (customerEmail == null)
        {
            context.Result = new UnauthorizedObjectResult("Please log in as a chef.");
            return;
        }

        var customer = await _chefService.GetPersonAsync(customerEmail);

        if (customer == null)
        {
            context.Result = new UnauthorizedObjectResult("Please log in as a chef.");
            return;
        }

        await next();
    }
}