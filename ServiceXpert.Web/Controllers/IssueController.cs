using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;
using ServiceXpert.Web.Factories;
using ServiceXpert.Web.Filters;
using ServiceXpert.Web.Helpers;
using ServiceXpert.Web.ViewModels;
using System.Net;
using SxpEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Web.Controllers
{
    [Route("Issues")]
    public class IssueController(
        IHttpClientFactory httpClientFactory,
        ICompositeViewEngine compositeViewEngine) : SxpController(compositeViewEngine)
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        private List<string> NavigationTabs { get; } = ["All", "Open", "Resolved", "Closed"];

        private List<string> TableHeaders { get; } = ["Key", "Summary", "Assignee", "Reporter", "Created", "Priority", "Status"];

        [AjaxOperation]
        [HttpGet(nameof(InitializeCreateIssue))]
        public IActionResult InitializeCreateIssue()
        {
            return PartialView("_CreateIssueModal", new CreateIssueViewModel()
            {
                IssuePriorities = Enum.GetValues(typeof(SxpEnums.IssuePriority))
                    .Cast<SxpEnums.IssuePriority>().ToDictionary(p => (int)p, p => p.ToString())
            });
        }

        [AjaxOperation]
        [HttpPost]
        public async Task<IActionResult> CreateIssueAsync(IssueDataObjectForCreate issue)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
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
            return base.View(new IssueViewModel()
            {
                NavigationTabs = this.NavigationTabs,
                TableHeaders = this.TableHeaders
            });
        }

        [AjaxOperation]
        [HttpGet(nameof(GetPagedIssuesAsync))]
        public async Task<IActionResult> GetPagedIssuesAsync(string tab, int pageNumber = 1, int pageSize = 10)
        {
            using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues?Status={tab}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var (issues, pagination) = HttpContentFactory.DeserializeContent<(IEnumerable<Issue>, Pagination)>(response);

            var tabContentView = await RenderViewToHtmlStringAsync("_IssueTableRow", issues.ToList());
            var paginationView = await RenderViewToHtmlStringAsync("_Pagination", pagination, GetPaginationViewDataDictionary(pagination, this.ModelState));

            return Json(new { tabContentView, paginationView });
        }

        [AjaxOperation]
        [HttpGet("{issueKey}")]
        public async Task<IActionResult> ViewIssueAsync(string issueKey)
        {
            try
            {
                _ = int.TryParse(issueKey.Split('-')[1], out _);
            }
            catch (IndexOutOfRangeException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue Key: {issueKey}");
            }

            using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var issue = HttpContentFactory.DeserializeContent<Issue>(response);

            return PartialView("~/Views/Issue/_ViewIssueModal.cshtml", issue);
        }
    }
}