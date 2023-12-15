using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using neighbor_chef.Services;

namespace neighbor_chef.Filters;

public class CustomerAuthorizeAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var customerService = context.HttpContext.RequestServices.GetService(typeof(ICustomerService)) as ICustomerService;
        
        if (customerService == null)
        {
            context.Result = new UnauthorizedObjectResult("Customer service not found.");
            return;
        }
        
        var user = context.HttpContext.User;
        var customerEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (customerEmail == null)
        {
            context.Result = new UnauthorizedObjectResult("Please log in as a customer.");
            return;
        }

        var customer = await customerService.GetPersonAsync(customerEmail);

        if (customer == null)
        {
            context.Result = new UnauthorizedObjectResult("Please log in as a customer.");
            return;
        }

        await next();
    }
}