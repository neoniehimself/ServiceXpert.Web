using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Enums.Security;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers;
[Route("Users")]
public class UserController : SxpController
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOrUser))]
    [HttpGet("SearchUserByName")]
    public async Task<IActionResult> SearchUserByNameAsync(string searchQuery)
    {
        using var httpClient = this.httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Users/SearchUserByName?searchQuery={searchQuery}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<SecurityProfile>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Ok(new { userProfiles = apiResponse.Value });
    }
}
