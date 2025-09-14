using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Enums;
using ServiceXpert.Web.ViewModels;
using System.Diagnostics;

namespace ServiceXpert.Web.Controllers;
[Authorize(Policy = nameof(Policy.User))]
public class HomeController : SxpController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
