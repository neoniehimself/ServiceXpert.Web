using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Application.Abstractions.Concretes.Services;
using ServiceXpert.Application.Abstractions.Interfaces.Services;

namespace ServiceXpert.Application.Shared
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.TryAddScoped<IIssueService, IssueService>();

            return services;
        }
    }
}
