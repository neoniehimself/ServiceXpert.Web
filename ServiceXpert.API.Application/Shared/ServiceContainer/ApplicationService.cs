using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Api.Application.Abstractions.Concretes.Services;
using ServiceXpert.Api.Application.Abstractions.Interfaces.Services;

namespace ServiceXpert.Api.Application.Shared.ServiceContainer
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.TryAddScoped<IIssueService, IssueService>();

            return services;
        }
    }
}
