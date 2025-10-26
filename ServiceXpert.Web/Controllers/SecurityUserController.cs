using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Enums.Security;

namespace ServiceXpert.Web.Controllers;
[Route("Security/Users")]
public class SecurityUserController : SxpController
{
    [HttpGet("")]
    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    public IActionResult Index()
    {
        return View();
    }
}
