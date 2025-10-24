using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Issues;
using ServiceXpert.Web.Utils;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues/{issueKey}/Comments")]
public class CommentController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey, [FromServices] ICompositeViewEngine compositeViewEngine)
    {
        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/Comments");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<IssueComment>>>(response);

        if (!apiResponse!.IsSuccess)
        {

        }

        if (apiResponse.Value == null || apiResponse.Value.Count == 0)
        {
            return Json(new { hasComments = false });
        }

        var commentsHtml = await RenderViewToHtmlStringAsync(compositeViewEngine, "~/Views/Shared/_CommentsSectionRow.cshtml", apiResponse.Value);
        return Json(new { hasComments = true, commentsHtml });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(string issueKey, CreateIssueComment comment)
    {
        if (!IssueUtil.IsKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid issue: {issueKey}");
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/Comments", HttpContentUtil.SerializeContentWithApplicationJson(comment));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Json(new { });
    }
}
