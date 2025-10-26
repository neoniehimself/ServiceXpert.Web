using Microsoft.Extensions.Caching.Memory;
using ServiceXpert.Web.Models;
using ServiceXpert.Web.Models.Security;
using ServiceXpert.Web.Utils;
using System.Security.Claims;

namespace ServiceXpert.Web;
public class IdentityClaimsMiddleware(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IConfiguration configuration, RequestDelegate requestDelegate)
{
    private readonly IHttpClientFactory httpClientFactory = httpClientFactory;
    private readonly IMemoryCache memoryCache = memoryCache;
    private readonly IConfiguration configuration = configuration;
    private readonly RequestDelegate requestDelegate = requestDelegate;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            var profileId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(profileId))
            {
                await this.requestDelegate(httpContext);
                return;
            }

            var firstName = await this.memoryCache.GetOrCreateAsync($"firstName_{profileId}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(this.configuration["Jwt:ExpiresInMinutes"]));

                var httpClient = this.httpClientFactory.CreateClient();
                var httpResponse = await httpClient.GetAsync($"{httpClient.BaseAddress}/Security/Users/Profiles/{profileId}");
                httpResponse.EnsureSuccessStatusCode();

                var apiResponse = await HttpContentUtil.DeserializeContentAsync<ApiResponse<SecurityProfile>>(httpResponse);
                return apiResponse!.Value.FirstName;
            });

            var identity = (ClaimsIdentity)httpContext.User.Identity;
            if (!identity.HasClaim(c => c.Type == ClaimTypes.GivenName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName!));
            }
        }

        await this.requestDelegate(httpContext);
    }
}
