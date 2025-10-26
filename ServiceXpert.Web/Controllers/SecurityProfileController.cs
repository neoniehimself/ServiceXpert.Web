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
        var httpClient = this.httpClientFactory.CreateClient();
        var httpResponse = await httpClient.GetAsync($"Security/Users/Profiles/SearchProfileByName?Name={name}");
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<List<SecurityProfile>>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {

        }

        return Ok(new { profiles = apiResponse.Value });
    }
}
