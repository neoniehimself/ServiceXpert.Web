using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers;
public class AccountController : Controller
{
    public IActionResult Index(bool isAuthenticated = false)
    {
        if (isAuthenticated)
        {
            this.ViewData["IsAuthenticated"] = isAuthenticated;
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}
