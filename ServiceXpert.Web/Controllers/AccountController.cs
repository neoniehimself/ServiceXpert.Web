using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;

namespace ServiceXpert.Web.Controllers;
[Route("")] // Tell the framework that this is the entry point
[Route("Accounts")]
public class AccountController(IHttpClientFactory httpClientFactory) : SxpController
{
    [AllowAnonymous]
    [HttpGet("")] // Entry point method
    public IActionResult Index()
    {
        if (!string.IsNullOrWhiteSpace(this.BearerToken))
        {
            return Redirect("Dashboard");
        }

        return View();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> LoginAsync(Login login, [FromServices] IConfiguration configuration)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        using var httpClient = httpClientFactory.CreateClient(HttpClientSettings.AuthHttpClientSettings);
        using var httpResponse = await httpClient.PostAsync($"{httpClient.BaseAddress}/Accounts/Login", HttpContentUtil.SerializeContentWithApplicationJson(login));
        var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<string>>(httpResponse);

        if (!apiResponse!.IsSuccess)
        {
            return BadRequest(apiResponse.Errors);
        }

        this.Response.Cookies.Append(AuthSettings.BearerTokenCookieName, apiResponse.Value, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt16(configuration["Jwt:ExpiresInMinutes"])).UtcDateTime
        });

        return Json(new { redirectUrl = "/Dashboard" });
    }

    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        this.Response.Cookies.Delete(AuthSettings.BearerTokenCookieName);
        return Redirect("/");
    }
}
