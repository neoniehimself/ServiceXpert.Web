using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Enums.Issues;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Issues;
using ServiceXpert.Web.Utils;
using ServiceXpert.Web.ValueObjects;
using ServiceXpert.Web.ViewModels.Issues;
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
            StatusCategories = SxpEnumUtil.ToList<IssueStatusCategory>(),
        });
    }

    [HttpGet("GetPagedIssuesByStatus")]
    public async Task<IActionResult> GetPagedIssuesByStatusAsync([FromServices] ICompositeViewEngine compositiveViewEngine, string statusCategory = "All", int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.GetAsync(string.Format($"Issues?StatusCategory={statusCategory}&PageNumber={pageNumber}&PageSize={pageSize}"), cancellationToken);
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<PaginationResult<Issue>>>(httpResponse);

        var issuesTableRowsHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "~/Views/Issue/_IssuesTableRow.cshtml", apiResponse!.Value.Items);
        var paginationHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "~/Views/Shared/_Pagination.cshtml", apiResponse.Value.Pagination, GetPaginationViewDataDictionary(apiResponse.Value.Pagination, this.ModelState));

        return Json(new { issuesTableRowsHtml, paginationHtml });
    }

    [HttpGet("View/{issueKey}", Name = "ViewIssue")]
    public async Task<IActionResult> ViewIssueAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            this.TempData[this.TempDataErrorKey] = "You are trying to access an invalid issue. Issue: " + issueKey;
            return RedirectToError();
        }

        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.GetAsync($"Issues/{issueKey}", cancellationToken);
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<Issue>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    this.TempData[this.TempDataErrorKey] = "The issue you are trying to access does not exists. Issue: " + issueKey;
                    return RedirectToError();
            }
        }

        return View("~/Views/Issue/ViewIssue.cshtml", apiResponse.Value);
    }

    [HttpGet("Edit/{issueKey}", Name = "EditIssue")]
    public async Task<IActionResult> EditIssueAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            this.TempData[this.TempDataErrorKey] = "You are trying to access an invalid issue. Issue: " + issueKey;
            return RedirectToError();
        }

        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.GetAsync($"Issues/{issueKey}", cancellationToken);
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<Issue>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    this.TempData[this.TempDataErrorKey] = "The issue you are trying to access does not exists. Issue: " + issueKey;
                    return RedirectToError();
            }
        }

        return View("~/Views/Issue/EditIssue.cshtml", new EditIssueViewModel()
        {
            Issue = apiResponse.Value,
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.Issues.IssuePriority>(),
            IssueStatuses = SxpEnumUtil.ToDictionary<Enums.Issues.IssueStatus>()
        });
    }

    [HttpPut("Edit/{issueKey}")]
    public async Task<IActionResult> UpdateIssueAsync(string issueKey, UpdateIssue updateIssue, CancellationToken cancellationToken = default)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            return BadRequest("You are trying to update an invalid issue: Issue: " + issueKey);
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.PutAsync($"Issues/{issueKey}", HttpContentUtil.SerializeContentWithApplicationJson(updateIssue), cancellationToken);
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { statusCode = HttpStatusCode.NoContent });
    }

    [HttpGet("InitializeCreateIssue")]
    public IActionResult InitializeCreateIssue()
    {
        return PartialView("~/Views/Shared/_CreateIssueModal.cshtml", new CreateIssueViewModel()
        {
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.Issues.IssuePriority>()
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssueAsync(CreateIssue createIssue, CancellationToken cancellationToken = default)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.PostAsync($"Issues", HttpContentUtil.SerializeContentWithApplicationJson(createIssue), cancellationToken);
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<string>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { issueKey = $"{IssuePreFix.SXP}-{apiResponse.Value}" });
    }
}
