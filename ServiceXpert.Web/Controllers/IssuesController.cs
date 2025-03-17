using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.ViewModels;
using System.Text;
using SharedEnums = ServiceXpert.Shared.Enums;

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
        public IActionResult InitializeCreateIssue()
        {
            var viewModel = new CreateIssueViewModel();

            viewModel.IssuePriorities = Enum.GetValues(typeof(SharedEnums.IssuePriority))
                .Cast<SharedEnums.IssuePriority>()
                .ToDictionary(p => (int)p, p => p.ToString());

            return PartialView("_CreateIssueModal", viewModel);
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
        public async Task<IActionResult> AllIssues()
        {
            var httpClient = this.httpClientFactory.CreateClient("Default");

            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var issues = new List<Issue>(JsonConvert.DeserializeObject<List<Issue>>(result)!);

                return PartialView("~/Views/Issues/_AllIssues.cshtml", issues);
            }

            return new StatusCodeResult((int)response.StatusCode);
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