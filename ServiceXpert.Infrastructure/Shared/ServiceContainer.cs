using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Infrastructure.Abstractions.Concretes.Repositories;
using ServiceXpert.Infrastructure.Contexts;

namespace ServiceXpert.Infrastructure.Shared
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<SxpDbContext>();
            services.TryAddScoped<IIssueRepository, IssueRepository>();

            return services;
        }
    }
}
