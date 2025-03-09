using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceXpert.Web.ViewModels;
using System.Diagnostics;

namespace ServiceXpert.Web.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> CreateIssue()
    {
        var httpClient = this.httpClientFactory.CreateClient("Default");
        var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issue/IssuePriorities");

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var issuePriorities = new List<string>(JsonConvert.DeserializeObject<List<string>>(result)!);

            var viewModel = new CreateIssueViewModel()
            {
                IssuePriorities = issuePriorities
            };

            return PartialView("_CreateIssueModal", viewModel);
        }

        return new StatusCodeResult((int)response.StatusCode);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
