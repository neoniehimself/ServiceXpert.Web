using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Web;
using ServiceXpert.Web.Constants;
using ServiceXpert.Web.Enums;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection(
                    nameof(ServiceXpertConfiguration)).Get<ServiceXpertConfiguration>()!.JwtSecretKey
            )
        );

        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey(AuthSettings.BearerTokenCookieName))
                {
                    context.Token = context.Request.Cookies[AuthSettings.BearerTokenCookieName];
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.ContentType = "text/html";
                return context.Response.WriteAsync(@"
                    <script>
                        alert('Please log in to continue.');
                        window.location.href = '/';
                    </script>
                ");
            }
        };
    });

var authBuilder = builder.Services.AddAuthorizationBuilder();
authBuilder.AddPolicy(nameof(Policy.AdminOnly), policy => policy.RequireRole(nameof(Role.Admin)));
authBuilder.AddPolicy(nameof(Policy.UserOnly), policy => policy.RequireRole(nameof(Role.User)));

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<BearerTokenHandler>();

var uri = new Uri(builder.Configuration["ApiSettings:Url"] ?? throw new NullReferenceException("Fatal: Missing Api Url"));
builder.Services.AddHttpClient(HttpClientSettings.AuthHttpClientSettings, client => client.BaseAddress = uri);
builder.Services.AddHttpClient(HttpClientSettings.Default, client => client.BaseAddress = uri).AddHttpMessageHandler<BearerTokenHandler>();

builder.Services
    .AddControllersWithViews(options =>
    {
        options.ReturnHttpNotAcceptable = true;

        // Global Authorization Filter
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddNewtonsoftJson();

var app = builder.Build();

/* Add rewriter to redirect to a path without a trailing slash because search engines treat these routes as different
 * Example:
 *      Original:  https://servicexpert.com/ 
 *      Rewritten: https://servicexpert.com
*/
app.UseRewriter(new RewriteOptions().AddRedirect("(.*)/$", "$1", 301));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
