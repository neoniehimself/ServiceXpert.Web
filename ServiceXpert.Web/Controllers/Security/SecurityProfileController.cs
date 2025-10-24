using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers.Security;
[Route("SecurityProfiles")]
public class SecurityProfileController : SxpController
{
    private readonly IHttpClientFactory httpClientFactory;

    public SecurityProfileController(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    [HttpGet("SearchProfileByName")]
    public async Task<IActionResult> SearchProfileByNameAsync(string name)
    {
        using var httpClient = this.httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/SecurityProfiles/SearchProfileByName?name={name}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<SecurityProfile>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Ok(new { securityProfiles = apiResponse.Value });
    }
}
