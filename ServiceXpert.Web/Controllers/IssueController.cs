using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using ServiceXpert.Web.Factories;
using ServiceXpert.Web.Filters;
using ServiceXpert.Web.Helpers;
using ServiceXpert.Web.ViewModels;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers
{
    [Route("Issues")]
    public class IssueController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        [AjaxOperation]
        [HttpGet("InitializeCreateIssue")]
        public IActionResult InitializeCreateIssue()
        {
            return PartialView("_CreateIssueModal", new CreateIssueViewModel()
            {
                IssuePriorities = Enum.GetValues(typeof(SxpEnums.IssuePriority))
                    .Cast<SxpEnums.IssuePriority>().ToDictionary(p => (int)p, p => p.ToString())
            });
        }

        [AjaxOperation]
        [HttpPost("CreateIssue")]
        public async Task<IActionResult> CreateIssue(IssueDataObjectForCreate issue)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues", HttpContentFactory.SerializeContent(issue));

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            return Json(new { issueKey = response.Content.ReadAsStringAsync().Result });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AjaxOperation]
        [HttpGet("GetTabContent")]
        public async Task<IActionResult> GetTabContent(string tab, int pageNumber = 1, int pageSize = 10)
        {
            var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues?Status={tab}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var (issues, paginationMetaData) = HttpContentFactory.DeserializeContent<(IEnumerable<Issue>, PaginationMetadata)>(response);

            int startPage = Math.Max(1, paginationMetaData.CurrentPage - 2);
            int endPage = Math.Min(paginationMetaData.TotalPageCount, startPage + 4);

            if (endPage - startPage < 4)
            {
                startPage = Math.Max(1, endPage - 4);
            }

            this.ViewData["PaginationStartPage"] = startPage;
            this.ViewData["PaginationEndPage"] = endPage;

            this.Response.Headers.Append("SXP-Issues-Tab-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetaData));
            return PartialView("~/Views/Issue/_TabContent.cshtml", new IssueListPageViewModel()
            {
                Issues = issues.ToList(),
                Metadata = paginationMetaData
            });
        }

        [HttpGet("{issueKey}", Name = "IssueDetails")]
        public async Task<IActionResult> Details(string issueKey)
        {
            try
            {
                _ = int.TryParse(issueKey.Split('-')[1], out _);
            }
            catch (IndexOutOfRangeException)
            {
                return NotFound();
            }

            var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var issue = HttpContentFactory.DeserializeContent<Issue>(response);

            return View(new IssuePageViewModel(issue!));
        }
    }
}