using Microsoft.AspNetCore.Mvc;

namespace ServiceXpert.Web.Controllers
{
    public class IssuesController : Controller
    {
        public IActionResult AllIssues()
        {
            return View();
        }
    }
}
