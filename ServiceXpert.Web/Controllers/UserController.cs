using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Models.AspNetUserProfile;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers;

[Authorize]
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
        using var response = await httpClient.GetAsync($"{httpClient.BaseAddress}/Users/SearchUserByName?searchQuery={searchQuery}");

        if (!response.IsSuccessStatusCode)
        {
            var errors = await HttpContentUtil.DeserializeContentAsync<List<string>>(response);
            return StatusCode((int)response.StatusCode, errors);
        }

        var userProfiles = await HttpContentUtil.DeserializeContentAsync<ICollection<AspNetUserProfile>>(response);
        return Ok(new { userProfiles });
    }
}
