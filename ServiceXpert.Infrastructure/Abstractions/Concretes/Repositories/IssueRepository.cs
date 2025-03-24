using ServiceXpert.Domain.Abstractions.Repositories;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Infrastructure.Contexts;

namespace ServiceXpert.Infrastructure.Abstractions.Concretes.Repositories
{
    public class IssueRepository(SxpDbContext dbContext) : RepositoryBase<int, Issue>(dbContext), IIssueRepository
    {
    }
}
