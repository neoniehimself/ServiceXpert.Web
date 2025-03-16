using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceXpert.Web.Models.Issues;
using ServiceXpert.Web.ViewModels;
using System.Text;

namespace ServiceXpert.Web.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public IssuesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> InitializeCreateIssue()
        {
            var httpClient = this.httpClientFactory.CreateClient("Default");
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/IssuePriorities");

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

        [HttpPost]
        public async Task<IActionResult> CreateIssue(IssueForCreate issue)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var httpClient = this.httpClientFactory.CreateClient("Default");

            var serializedObject = JsonConvert.SerializeObject(issue);
            var requestContent = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { });
            }

            return new StatusCodeResult((int)response.StatusCode);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllIssues()
        {
            return PartialView("~/Views/Issues/_AllIssues.cshtml");
        }

        [HttpGet]
        public IActionResult OpenIssues()
        {
            return PartialView("~/Views/Issues/_OpenIssues.cshtml");
        }

        [HttpGet]
        public IActionResult ResolvedIssues()
        {
            return PartialView("~/Views/Issues/_ResolvedIssues.cshtml");
        }
    }
}

// TODO: Implement Shared.Enums (IssuePriority for CreateIssue)