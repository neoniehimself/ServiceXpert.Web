using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ServiceXpert.Web.Models.Comment;
using ServiceXpert.Web.Utils;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues/{issueKey}/Comments")]
public class CommentController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey, [FromServices] ICompositeViewEngine compositeViewEngine)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue key: {issueKey}");
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/Comments");

        if (!response.IsSuccessStatusCode)
        {
            var errors = await HttpContentUtil.DeserializeContentAsync<IEnumerable<string>>(response);
            return StatusCode((int)response.StatusCode, errors);
        }

        var comments = await HttpContentUtil.DeserializeContentAsync<List<Comment>>(response);

        if (comments == null || comments.Count == 0)
        {
            return Json(new { hasComments = false });
        }

        var commentsHtml = await RenderViewToHtmlStringAsync(compositeViewEngine, "~/Views/Shared/_CommentsSectionRow.cshtml", comments!);
        return Json(new { hasComments = true, commentsHtml });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(string issueKey, CommentForCreate comment)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue key: {issueKey}");
        }

        using var httpClient = httpClientFactory.CreateClient();
        using var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/Comments", HttpContentUtil.SerializeContentWithApplicationJson(comment));

        if (!response.IsSuccessStatusCode)
        {
            var errors = await HttpContentUtil.DeserializeContentAsync<IEnumerable<string>>(response);
            return StatusCode((int)response.StatusCode, errors);
        }

        return Json(new { });
    }
}
