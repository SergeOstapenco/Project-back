using Microsoft.AspNetCore.Authorization;

namespace Backend;

public class UserModAttribute : AuthorizeAttribute
{
    public UserModAttribute() : base()
    {
        Roles = "user,admin";
    }
}
