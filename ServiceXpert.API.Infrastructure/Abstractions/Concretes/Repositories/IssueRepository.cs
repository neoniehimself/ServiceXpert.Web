using ServiceXpert.Api.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.Api.Domain.Entities;
using ServiceXpert.Api.Infrastructure.DbContexts;

namespace ServiceXpert.Api.Infrastructure.Abstractions.Concretes.Repositories
{
    public class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
    {
        public IssueRepository(SXPDbContext dbContext) : base(dbContext)
        {
        }
    }
}
