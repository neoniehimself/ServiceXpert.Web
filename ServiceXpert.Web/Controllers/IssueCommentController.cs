using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Issues;
using ServiceXpert.Web.Utils;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues/{issueKey}/Comments")]
public class IssueCommentController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey, [FromServices] ICompositeViewEngine compositeViewEngine)
    {
        var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync($"Issues/{issueKey}/Comments");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<IssueComment>>>(response);

        if (!apiResponse!.IsSuccess)
        {

        }

        if (apiResponse.Value == null || apiResponse.Value.Count == 0)
        {
            return Json(new { hasComments = false });
        }

        var issueCommentsHtml = await RenderViewToHtmlStringAsync(compositeViewEngine, "~/Views/Issue/_IssueCommentsSectionRow.cshtml", apiResponse.Value);
        return Json(new { hasIssueComments = true, issueCommentsHtml });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(string issueKey, CreateIssueComment createIssueComment)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid issue: {issueKey}");
        }

        var httpClient = httpClientFactory.CreateClient();
        var httpResponse = await httpClient.PostAsync($"Issues/{issueKey}/Comments", HttpContentUtil.SerializeContentWithApplicationJson(createIssueComment));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<Guid>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { });
    }
}
