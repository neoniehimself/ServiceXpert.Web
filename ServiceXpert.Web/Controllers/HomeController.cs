using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers;
[Route("Home")]
public class HomeController : SxpController
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
