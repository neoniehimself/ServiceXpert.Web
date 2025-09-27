using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.ViewModels;

namespace ServiceXpert.Web.Controllers;
[Route("Errors")]
public class ErrorController : SxpController
{
    [HttpGet("")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        var error = this.TempData[this.TempDataErrorKey];
        var viewModel = new ErrorViewModel();

        // Will only be null if the error did not come from a controller
        if (error == null)
        {
            return View(viewModel);
        }

        this.ViewData["IsErrorFromController"] = true;
        if (error is string @string)
        {
            this.ViewData["IsStringError"] = true;
            viewModel.Error = @string;
        }
        else if (error is IEnumerable<string> list)
        {
            this.ViewData["IsStringError"] = false;
            viewModel.Errors = list;
        }

        return View(viewModel);
    }
}
