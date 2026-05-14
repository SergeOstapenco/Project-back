using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend;

public class UserModAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userRole = context.HttpContext.Request.Headers["Role"].ToString();
        var isUser = string.Equals(userRole, "User", StringComparison.OrdinalIgnoreCase);
        var isAdmin = string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase);

        if (!isUser && !isAdmin)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Forbidden: User or Admin role required"
            };
        }

        base.OnActionExecuting(context);
    }
}
