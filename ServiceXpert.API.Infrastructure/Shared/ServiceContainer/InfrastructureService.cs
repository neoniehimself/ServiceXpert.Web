using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Infrastructure.Abstractions.Concretes.Repositories;
using ServiceXpert.Api.Infrastructure.DbContexts;

namespace ServiceXpert.Api.Infrastructure.Shared.ServiceContainer
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<SXPDbContext>();

            services.TryAddScoped<IIssueRepository, IssueRepository>();

            return services;
        }
    }
}
