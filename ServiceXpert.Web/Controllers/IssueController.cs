using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Utils;
using ServiceXpert.Web.ViewModels;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues")]
public class IssueController(IHttpClientFactory httpClientFactory, ICompositeViewEngine compositeViewEngine)
    : SxpController(compositeViewEngine)
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    [HttpGet]
    public IActionResult Index()
    {
        return base.View(new IssueViewModel()
        {
            StatusCategories = SxpEnumUtil.ToList<Enums.IssueStatusCategory>(),
        });
    }

    [HttpGet("GetPagedIssuesByStatusAsync")]
    public async Task<IActionResult> GetPagedIssuesByStatusAsync(
        string statusCategory = "All", int pageNumber = 1, int pageSize = 10)
    {
        using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);

        var response = await httpClient.GetAsync(
            string.Format("{0}/Issues?StatusCategory={1}&PageNumber={2}&PageSize={3}",
                httpClient.BaseAddress, statusCategory, pageNumber, pageSize)
        );

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var result = HttpContentUtil.DeserializeContent<PagedIssuesResponse>(response);

        var issueTableRowsHtml = await RenderViewToHtmlStringAsync("_IssueTableRow", result!.Issues);

        var paginationHtml = await RenderViewToHtmlStringAsync("_Pagination", result.Pagination,
            GetPaginationViewDataDictionary(result.Pagination, this.ModelState));

        return Json(new { issueTableRowsHtml, paginationHtml });
    }

    [HttpGet("ViewIssueAsync/{issueKey}")]
    public async Task<IActionResult> ViewIssueAsync(string issueKey)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue Key: {issueKey}");
        }

        using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
        var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var issue = HttpContentUtil.DeserializeContent<Issue>(response);

        return PartialView("~/Views/Issue/_ViewIssueModal.cshtml", issue);
    }

    [HttpGet("EditIssueAsync/{issueKey}")]
    public async Task<IActionResult> EditIssueAsync(string issueKey)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue Key: {issueKey}");
        }

        using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
        var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var issue = HttpContentUtil.DeserializeContent<Issue>(response);

        return PartialView("~/Views/Issue/_EditIssueModal.cshtml", new EditIssueViewModel(issue!)
        {
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.IssuePriority>(),
            IssueStatuses = SxpEnumUtil.ToDictionary<Enums.IssueStatus>()
        });
    }

    [HttpPut("UpdateIssueAsync/{issueKey}")]
    public async Task<IActionResult> UpdateIssueAsync(string issueKey, IssueForUpdate issue)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue Key: {issueKey}");
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
        var response = await httpClient.PutAsync($"{httpClient.BaseAddress}/Issues/{issueKey}",
            HttpContentUtil.SerializeContentWithApplicationJson(issue));

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        return Json(new { statusCode = 204 });
    }

    [HttpGet("InitializeCreateIssue")]
    public IActionResult InitializeCreateIssue()
    {
        return PartialView("_CreateIssueModal", new CreateIssueViewModel()
        {
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.IssuePriority>()
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssueAsync(IssueForCreate issue)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        using var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
        var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues",
            HttpContentUtil.SerializeContentWithApplicationJson(issue));

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        return Json(new { issueKey = response.Content.ReadAsStringAsync().Result });
    }
}