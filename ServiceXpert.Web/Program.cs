using Microsoft.AspNetCore.Rewrite;
using ServiceXpert.Web.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(ApiSettings.Name, configureClient =>
{
    var url = builder.Configuration[ApiSettings.Url] ?? throw new NullReferenceException("Missing Api Url");
    configureClient.BaseAddress = new Uri(url);
});

builder.Services.AddControllersWithViews(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson();

var app = builder.Build();

/* Add rewriter to redirect to a path without a trailing slash because search engines treat these routes as different
 * Example:
 *      Original:  https://servicexpert.com/ 
 *      Rewritten: https://servicexpert.com
*/
app.UseRewriter(new RewriteOptions().AddRedirect("(.*)/$", "$1"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Account}/{action=Index}/{id?}"
);

app.Run();
