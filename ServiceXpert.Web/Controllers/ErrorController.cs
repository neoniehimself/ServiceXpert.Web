using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.ViewModels;

namespace ServiceXpert.Web.Controllers;
[Route("Error")]
public class ErrorController : SxpController
{
    [HttpGet("")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var error = this.TempData[this.TempDataErrorKey];
        var viewModel = new ErrorViewModel();

        // Will only be null if the error did not come from a controller
        if (error == null)
        {
            return View(viewModel);
        }

        this.ViewData["IsErrorFromController"] = true;
        if (error is string errorString)
        {
            this.ViewData["IsErrorString"] = true;
            viewModel.ErrorString = errorString;
        }
        else if (error is IEnumerable<string> errorList)
        {
            this.ViewData["IsErrorString"] = false;
            viewModel.ErrorList = errorList;
        }

        return View(viewModel);
    }
}
