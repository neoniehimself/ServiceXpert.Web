using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.ViewModels;
using System.Text;

namespace ServiceXpert.Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public IssueController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> InitializeCreateIssue()
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
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/issue", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { });
            }

            return new StatusCodeResult((int)response.StatusCode);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
