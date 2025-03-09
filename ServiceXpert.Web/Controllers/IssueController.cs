using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.ViewModels;

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
        public Task<IActionResult> CreateIssue(IssueForCreate issue)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
