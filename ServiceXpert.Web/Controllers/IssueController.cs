using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers
{
    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
