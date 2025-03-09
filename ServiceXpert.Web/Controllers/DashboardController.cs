using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
