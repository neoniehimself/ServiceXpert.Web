using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security.Auth;
using ServiceXpert.Web.Utils;
using System.Security.Claims;

namespace ServiceXpert.Web.Controllers;
[Route("")] // Tell the framework that this is the entry point
[Route("Security/Accounts")]
public class AccountController(IHttpClientFactory httpClientFactory) : SxpController
{
    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        if (!string.IsNullOrWhiteSpace(this.BearerToken))
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> LoginAsync(LoginUser login, [FromServices] IConfiguration configuration)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var httpClient = httpClientFactory.CreateClient(HttpClientSettings.AuthHttpClientSettings);
        var httpResponse = await httpClient.PostAsync($"{httpClient.BaseAddress}/Security/Accounts/Login", HttpContentUtil.SerializeContentWithApplicationJson(login));
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

        return Json(new { redirectUrl = "/Home" });
    }

    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout([FromServices] IMemoryCache memoryCache)
    {
        this.Response.Cookies.Delete(AuthSettings.BearerTokenCookieName);
        ClearCache(memoryCache);
        return Redirect("/");
    }

    [NonAction]
    private void ClearCache(IMemoryCache memoryCache)
    {
        var profileId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(profileId))
        {
            memoryCache.Remove($"firstName_{profileId}");
        }
    }
}
