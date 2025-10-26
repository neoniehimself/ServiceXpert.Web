using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers;
[Route("Security/Users/Profiles")]
public class SecurityProfileController(IHttpClientFactory httpClientFactory) : SxpController
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

    [HttpGet("SearchProfileByName")]
    public async Task<IActionResult> SearchProfileByNameAsync(string name)
    {
        using var httpClient = this.httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Security/Users/Profiles/SearchProfileByName?Name={name}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<SecurityProfile>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Ok(new { profiles = apiResponse.Value });
    }
}
