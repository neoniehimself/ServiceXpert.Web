using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Api.Application.Abstractions.Concretes.Services;
using ServiceXpert.Api.Application.Abstractions.Interfaces.Services;

namespace ServiceXpert.Api.Application.Shared
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
