using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using neighbor_chef.Services;

public class IsSameAttribute : ActionFilterAttribute
{
    private readonly string _parameterName;

    public IsSameAttribute(string parameterName)
    {
        _parameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var idFromRoute = (Guid)context.ActionArguments[_parameterName];
        Console.WriteLine("idFromRoute: " + idFromRoute);
        if (idFromRoute == Guid.Empty)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.BadRequestResult();
            return;
        }
        var emailFromUser = context.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        Console.WriteLine("emailFromUser: " + emailFromUser);
        if (emailFromUser == null)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            return;
        }
        
        var _personService = (IPersonService)context.HttpContext.RequestServices.GetService(typeof(IPersonService));
        var user = _personService.GetPersonAsync(emailFromUser, true);
        if (user.Result.Id == idFromRoute) return;
        context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
    }
}