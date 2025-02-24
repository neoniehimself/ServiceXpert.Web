using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Infrastructure.Abstractions.Concretes.Repositories;
using ServiceXpert.API.Infrastructure.DbContexts;

namespace ServiceXpert.API.Infrastructure.Shared.ServiceContainer
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<SXPDbContext>();

            services.TryAddScoped<IIssueRepository, IssueRepository>();
            services.TryAddScoped<IIssueStatusRepository, IssueStatusRepository>();

            return services;
        }
    }
}
