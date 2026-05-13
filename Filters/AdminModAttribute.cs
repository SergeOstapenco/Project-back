using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend;

public class AdminModAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userRole = context.HttpContext.Request.Headers["Role"].ToString();

        if (userRole != "Admin")
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Forbidden"
            };
        }

        base.OnActionExecuting(context);
    }
}