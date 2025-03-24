using ServiceXpert.Application.Shared;
using ServiceXpert.Infrastructure.Shared;
using ServiceXpert.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(ApiSettings.Name, configureClient =>
{
    var url = builder.Configuration[ApiSettings.Url] ?? throw new NullReferenceException("Missing API URL");
    configureClient.BaseAddress = new Uri(url);
});

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices();

builder.Services
    .AddControllersWithViews(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson();

var app = builder.Build();

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
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
