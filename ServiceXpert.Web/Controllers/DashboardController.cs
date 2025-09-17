using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers;
[Route("Dashboard")]
public class DashboardController : SxpController
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
