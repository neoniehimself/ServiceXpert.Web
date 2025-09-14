using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Enums;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers;
[Authorize(Policy = nameof(Policy.Admin))]
[Route("Accounts")]
public class AccountController(IHttpClientFactory httpClientFactory) : SxpController
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        // Temporary
        this.Response.Cookies.Delete(AuthSettings.Token);
        return View();
    }

    [AllowAnonymous]
    [HttpPost(nameof(LoginUserAsync))]
    public async Task<IActionResult> LoginUserAsync(LoginUser loginUser, [FromServices] IConfiguration configuration)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(GetModelStateErrors());
        }

        using var httpClient = httpClientFactory.CreateClient(ApiSettings.Name);
        using var response = await httpClient.PostAsync($"{httpClient.BaseAddress}/Accounts/LoginUserAsync", HttpContentUtil.SerializeContentWithApplicationJson(loginUser));

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest(await HttpContentUtil.DeserializeContentAsync<IEnumerable<string>>(response));
        }

        var token = await HttpContentUtil.GetResultAsStringAsync(response);
        this.Response.Cookies.Append(AuthSettings.Token, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt16(configuration["Jwt:ExpiresInMinutes"]))
        });

        return Json(new { redirectUrl = this.Url.Action("Index", "Home") });
    }
}
