using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Infrastructure.Contexts;

namespace ServiceXpert.Infrastructure.Abstractions.Concretes.Repositories
{
    public class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
    {
        public IssueRepository(SxpDbContext dbContext) : base(dbContext)
        {
        }
    }
}
