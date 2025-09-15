using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Enums;

namespace ServiceXpert.Web.Controllers;
[Authorize(Policy = nameof(Policy.User))]
[Route("Dashboard")]
public class DashboardController : SxpController
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
