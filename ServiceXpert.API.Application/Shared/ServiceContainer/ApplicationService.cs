using Microsoft.Extensions.DependencyInjection;

namespace ServiceXpert.API.Application.Shared.ServiceContainer
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
