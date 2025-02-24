using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;
using ServiceXpert.API.Infrastructure.DbContexts;

namespace ServiceXpert.API.Infrastructure.Abstractions.Concretes.Repositories
{
    public class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
    {
        public IssueRepository(SXPDbContext dbContext) : base(dbContext)
        {
        }
    }
}
