using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Enums;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Issue;
using ServiceXpert.Web.Utils;
using ServiceXpert.Web.ViewModels;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues")]
public class IssueController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return base.View(new IssueViewModel()
        {
            StatusCategories = SxpEnumUtil.ToList<Enums.IssueStatusCategory>(),
        });
    }

    [HttpGet("GetPagedIssuesByStatus")]
    public async Task<IActionResult> GetPagedIssuesByStatusAsync([FromServices] ICompositeViewEngine compositiveViewEngine, string statusCategory = "All", int pageNumber = 1, int pageSize = 10)
    {
        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync(string.Format("{0}/Issues?StatusCategory={1}&PageNumber={2}&PageSize={3}", httpClient.BaseAddress, statusCategory, pageNumber, pageSize));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<PagedResult<Issue>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        var issueTableRowsHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "_IssueTableRow", apiResponse.Value.Items);
        var paginationHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "_Pagination", apiResponse.Value.Pagination, GetPaginationViewDataDictionary(apiResponse.Value.Pagination, this.ModelState));

        return Json(new { issueTableRowsHtml, paginationHtml });
    }

    [HttpGet("View/{issueKey}", Name = "ViewIssue")]
    public async Task<IActionResult> ViewIssueAsync(string issueKey)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            this.TempData[this.TempDataErrorKey] = "You are trying to access an invalid issue. Issue: " + issueKey;
            return RedirectToError();
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<Issue>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return View("~/Views/Issue/ViewIssue.cshtml", apiResponse.Value);
    }

    [HttpGet("Edit/{issueKey}", Name = "EditIssue")]
    public async Task<IActionResult> EditIssueAsync(string issueKey)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            this.TempData[this.TempDataErrorKey] = "You are trying to access an invalid issue. Issue: " + issueKey;
            return RedirectToError();
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<Issue>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return View("~/Views/Issue/EditIssue.cshtml", new EditIssueViewModel()
        {
            Issue = apiResponse.Value,
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.IssuePriority>(),
            IssueStatuses = SxpEnumUtil.ToDictionary<Enums.IssueStatus>()
        });
    }

    [HttpPut("Edit/{issueKey}")]
    public async Task<IActionResult> UpdateIssueAsync(string issueKey, IssueForUpdate issue)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return BadRequest("You are trying to update an invalid issue: Issue: " + issueKey);
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PutAsync($"{httpClient.BaseAddress}/Issues/{issueKey}", HttpContentUtil.SerializeContentWithApplicationJson(issue));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { statusCode = HttpStatusCode.NoContent });
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
            return BadRequestInvalidModelState();
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues", HttpContentUtil.SerializeContentWithApplicationJson(issue));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<string>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { issueKey = $"{IssuePreFix.SXP}-{apiResponse.Value}" });
    }
}
