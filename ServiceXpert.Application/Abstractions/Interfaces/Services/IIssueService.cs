using FluentBuilder.Core;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared;

namespace ServiceXpert.Application.Abstractions.Interfaces.Services
{
    public interface IIssueService : IServiceBase<int, Issue>
    {
        Task<Issue?> GetByIssueKey(string issueKey, IncludeOptions<Issue>? includeOptions = null);

        Task<IEnumerable<Issue>> GetAllByStatusAsync(string status, IncludeOptions<Issue>? includeOptions = null);

        Task<(IEnumerable<Issue>, Pagination)> GetPagedAllByStatusAsync(
            string status, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null);

        Task UpdateByIssueKeyAsync(string issueKey, Issue issue);

        Task DeleteByIssueKeyAsync(string issueKey);

        Task<bool> IsExistsByIssueKeyAsync(string issueKey);

        int GetIdFromIssueKey(string issueKey);
    }
}
