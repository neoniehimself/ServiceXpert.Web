using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Models.Comment;
using ServiceXpert.Web.Utils;
using System.Net;

namespace ServiceXpert.Web.Controllers;
[Route("Issues/{issueKey}/Comments")]
public class CommentController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey)
    {
        if (!IssueUtil.IsIssueKeyValid(issueKey))
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Invalid Issue key: {issueKey}");
        }

        var httpClient = this.httpClientFactory.CreateClient(ApiSettings.Name);
        var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Issues/{issueKey}/Comments");

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode);
        }

        var comments = HttpContentUtil.DeserializeContent<List<Comment>>(response);
        return Json(new { comments });
    }
}
