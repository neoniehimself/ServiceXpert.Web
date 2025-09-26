using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.AspNetUserProfile;
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

    [HttpGet("SearchUserByName")]
    public async Task<IActionResult> SearchUserByNameAsync(string searchQuery)
    {
        using var httpClient = this.httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Users/SearchUserByName?searchQuery={searchQuery}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<AspNetUserProfile>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Ok(new { userProfiles = apiResponse.Value });
    }
}
