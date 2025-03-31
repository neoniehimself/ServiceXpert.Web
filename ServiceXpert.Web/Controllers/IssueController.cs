using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        ICompositeViewEngine compositeViewEngine) : Controller
    {
        private List<string> NavigationTabs { get; } = ["All", "Open", "Resolved", "Closed"];

        private List<string> TableHeaders { get; } = ["Key", "Summary", "Assignee", "Reporter", "Created", "Priority", "Status"];

        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;
        private readonly ICompositeViewEngine compositeViewEngine = compositeViewEngine;

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
        [HttpPost]
        public async Task<IActionResult> CreateIssue(IssueDataObjectForCreate issue)
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
        [HttpGet("GetTabContent")]
        public async Task<IActionResult> GetTabContent(string tab, int pageNumber = 1, int pageSize = 10)
        {
            using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
            var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues?Status={tab}&PageNumber={pageNumber}&PageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var (issues, pagination) = HttpContentFactory.DeserializeContent<(IEnumerable<Issue>, Pagination)>(response);

            var tabContentView = await RenderViewToHtmlStringAsync("_TabContent", issues.ToList());
            var paginationView = await RenderViewToHtmlStringAsync("_Pagination", pagination, GetPaginationViewDataDictionary(pagination, this.ModelState));

            return Json(new { tabContentView, paginationView });
        }

        [NonAction]
        private async Task<string> RenderViewToHtmlStringAsync(string viewName, object model, ViewDataDictionary? viewData = null)
        {
            var viewResult = this.compositeViewEngine.GetView(null, viewName, false);

            if (!viewResult.Success)
            {
                viewResult = this.compositeViewEngine.FindView(this.ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new InvalidOperationException($"View '{viewName}' not found.");
                }
            }

            using var writer = new StringWriter();

            viewData ??= new ViewDataDictionary(new EmptyModelMetadataProvider(), this.ModelState);
            viewData.Model = model;

            var viewContext = new ViewContext(
                this.ControllerContext,
                viewResult.View,
                viewData,
                this.TempData,
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }

        [NonAction]
        private static ViewDataDictionary GetPaginationViewDataDictionary(Pagination pagination, ModelStateDictionary modelState)
        {
            int startPage = Math.Max(1, pagination.CurrentPage - 2);
            int endPage = Math.Min(pagination.TotalPageCount, startPage + 4);

            if (endPage - startPage < 4)
            {
                startPage = Math.Max(1, endPage - 4);
            }

            return new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
            {
                Model = pagination,
                ["PaginationStartIndex"] = ((pagination.CurrentPage - 1) * pagination.PageSize) + 1,
                ["PaginationEndIndex"] = Math.Min(pagination.CurrentPage * pagination.PageSize, pagination.TotalCount),
                ["PaginationStartPage"] = startPage,
                ["PaginationEndPage"] = endPage
            };
        }

        [AjaxOperation]
        [HttpGet("ViewDetails")]
        public async Task<IActionResult> ViewDetails(string issueKey)
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

            return PartialView("~/Views/Issue/_ViewDetails.cshtml", issue);
        }
    }
}