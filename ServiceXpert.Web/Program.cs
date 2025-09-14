using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Web;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Enums;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection(
                nameof(ServiceXpertConfiguration)).Get<ServiceXpertConfiguration>()!.JwtSecretKey)
        )
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey(AuthSettings.Token))
            {
                context.Token = context.Request.Cookies[AuthSettings.Token];
            }
            return Task.CompletedTask;
        }
    };
});

var authBuilder = builder.Services.AddAuthorizationBuilder();
// Admin Policies
authBuilder.AddPolicy(nameof(Policy.Admin), policy => policy.RequireRole(nameof(Role.Admin)));
// User Policies
authBuilder.AddPolicy(nameof(Policy.User), policy => policy.RequireRole(nameof(Role.Admin), nameof(Role.User)));

builder.Services.AddHttpClient(ApiSettings.Name, configureClient =>
{
    var url = builder.Configuration[ApiSettings.Url] ?? throw new NullReferenceException("Fatal: Missing Api Url");
    configureClient.BaseAddress = new Uri(url);
});

builder.Services
    .AddControllersWithViews(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson();

builder.Services.Configure<RouteOptions>(options => options.AppendTrailingSlash = true);

var app = builder.Build();

/* Add rewriter to redirect to a path without a trailing slash because search engines treat these routes as different
 * Example:
 *      Original:  https://servicexpert.com/ 
 *      Rewritten: https://servicexpert.com
*/
// app.UseRewriter(new RewriteOptions().AddRedirect("(.*)/$", "$1"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Account}/{action=Index}/{id?}"
);

app.Run();
