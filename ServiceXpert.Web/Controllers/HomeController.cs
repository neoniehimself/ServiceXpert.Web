using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.ViewModels;
using System.Diagnostics;

namespace ServiceXpert.Web.Controllers;

public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
