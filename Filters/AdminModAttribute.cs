using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class AdminModAttribute : AuthorizeAttribute
{
    public AdminModAttribute() : base()
    {
        Roles = "admin";
    }
}
