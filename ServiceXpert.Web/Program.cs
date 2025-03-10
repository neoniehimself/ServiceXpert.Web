var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("Default", client =>
{
    var url = builder.Configuration["ApiSettings:Url"] ?? throw new KeyNotFoundException();
    client.BaseAddress = new Uri(url);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Main/Error");
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
