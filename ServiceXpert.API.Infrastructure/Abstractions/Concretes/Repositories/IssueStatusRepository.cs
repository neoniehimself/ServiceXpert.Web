using ServiceXpert.API.Domain.Abstractions.Interfaces.Repositories;
using ServiceXpert.API.Domain.Entities;
using ServiceXpert.API.Infrastructure.DbContexts;

namespace ServiceXpert.API.Infrastructure.Abstractions.Concretes.Repositories
{
    public class IssueStatusRepository : RepositoryBase<int, IssueStatus>, IIssueStatusRepository
    {
        public IssueStatusRepository(SXPDbContext dbContext) : base(dbContext)
        {
        }
    }
}
