using ServiceXpert.API.Application.Shared.ServiceContainer;
using ServiceXpert.API.Infrastructure.Shared.ServiceContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices();

builder.Services
    .AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
