using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.API.Application.Abstractions.Concretes.Services;
using ServiceXpert.API.Application.Abstractions.Interfaces.Services;

namespace ServiceXpert.API.Application.Shared.ServiceContainer
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.TryAddScoped<IIssueStatusService, IssueStatusService>();
            services.TryAddScoped<IIssueService, IssueService>();

            return services;
        }
    }
}
