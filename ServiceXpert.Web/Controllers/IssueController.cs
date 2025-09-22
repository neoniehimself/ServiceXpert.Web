using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
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
        using var response = await httpClient.GetAsync(string.Format("{0}/Issues?StatusCategory={1}&PageNumber={2}&PageSize={3}&IncludeCreatedByUser=true&IncludeAssignee=true", httpClient.BaseAddress, statusCategory, pageNumber, pageSize));

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var result = await HttpContentUtil.DeserializeContentAsync<PagedResult<Issue>>(response);
        var issueTableRowsHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "_IssueTableRow", result!.Items);
        var paginationHtml = await RenderViewToHtmlStringAsync(compositiveViewEngine, "_Pagination", result.Pagination, GetPaginationViewDataDictionary(result.Pagination, this.ModelState));

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
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

        if (!response.IsSuccessStatusCode)
        {
            this.TempData[this.TempDataErrorKey] = await HttpContentUtil.GetResultAsStringAsync(response);
            return RedirectToError();
        }

        var issue = await HttpContentUtil.DeserializeContentAsync<Issue>(response);
        return View("~/Views/Issue/ViewIssue.cshtml", issue);
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
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}");

        if (!response.IsSuccessStatusCode)
        {
            this.TempData[this.TempDataErrorKey] = await HttpContentUtil.GetResultAsStringAsync(response);
            return RedirectToError();
        }

        var issue = await HttpContentUtil.DeserializeContentAsync<Issue>(response);

        return View("~/Views/Issue/EditIssue.cshtml", new EditIssueViewModel()
        {
            Issue = issue!,
            IssuePriorities = SxpEnumUtil.ToDictionary<Enums.IssuePriority>(),
            IssueStatuses = SxpEnumUtil.ToDictionary<Enums.IssueStatus>()
        });
    }

    [HttpPut("Edit/{issueKey}")]
    public async Task<IActionResult> UpdateIssueAsync(string issueKey, IssueForUpdate issue)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return BadRequest("You are trying to update an invalid issue: Issue: " + issue);
        }

        if (!this.ModelState.IsValid)
        {
            var errors = GetModelStateErrors();
            return BadRequest(errors);
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.PutAsync($"{httpClient.BaseAddress}/Issues/{issueKey}", HttpContentUtil.SerializeContentWithApplicationJson(issue));

        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    var error = await HttpContentUtil.GetResultAsStringAsync(response);
                    return NotFound(error);
                case HttpStatusCode.BadRequest:
                    var errors = await HttpContentUtil.DeserializeContentAsync<IEnumerable<string>>(response);
                    return BadRequest(errors);
            }
        }

        return Json(new { statusCode = StatusCodes.Status204NoContent });
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
            var errors = GetModelStateErrors();
            return BadRequest(errors);
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues", HttpContentUtil.SerializeContentWithApplicationJson(issue));

        if (!response.IsSuccessStatusCode)
        {
            var errors = await HttpContentUtil.DeserializeContentAsync<IEnumerable<string>>(response);
            return BadRequest(errors);
        }

        return Json(new { issueKey = await HttpContentUtil.GetResultAsStringAsync(response) });
    }
}