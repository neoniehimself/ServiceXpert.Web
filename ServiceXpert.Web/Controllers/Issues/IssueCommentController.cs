using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Issues;
using ServiceXpert.Web.Utils;
using System.Net;

namespace ServiceXpert.Web.Controllers.Issues;
[Route("Issues/{issueKey}/IssueComments")]
public class IssueCommentController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey, [FromServices] ICompositeViewEngine compositeViewEngine)
    {
        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/IssueComments");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<IssueComment>>>(response);

        if (!apiResponse!.IsSuccess)
        {

        }

        bool hasIssueComments = true;

        if (apiResponse.Value == null || apiResponse.Value.Count == 0)
        {
            hasIssueComments = false;
            return Json(new { hasIssueComments });
        }

        var issueCommentsHtml = await RenderViewToHtmlStringAsync(compositeViewEngine, "~/Views/Shared/Issues/_IssueCommentsSectionRow.cshtml", apiResponse.Value);
        return Json(new { hasIssueComments, issueCommentsHtml });
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssueCommentAsync(string issueKey, CreateIssueComment createIssueComment)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid issue: {issueKey}");
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/IssueComments", HttpContentUtil.SerializeContentWithApplicationJson(createIssueComment));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { });
    }
}
